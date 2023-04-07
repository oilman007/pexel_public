using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Pexel.SCAL;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Pexel.Eclipse;



// System.IO.FileSystemWatcher

namespace Pexel.HM
{
    public class ResultsViewTreeDataDownloader
    {
        public ResultsViewTreeDataDownloader(string id)
        {
            ID = id;
        }

        public delegate void StateHandler(string message);
        public StateHandler MsgHandler;


        public TreeView TreeView { set; get; } = new TreeView();

        public Dictionary<string, ResultsViewDataOpt> Data { set; get; } = new Dictionary<string, ResultsViewDataOpt>();


        public string ID { set; get; } = string.Empty;




        const string settings_file_ext = ".pxlrv";
        const int refrash_delay_normal = 10000;





        public void AddFiles(params string[] pxlhm_files)
        {
            new Task(() =>
            {
                foreach (string pxlhm_file in pxlhm_files)
                    lock (Data)
                        if (!Data.ContainsKey(pxlhm_file))
                        {
                            HistMatchingInput.Load(pxlhm_file, out HistMatchingInput input_data);
                            MsgHandler?.Invoke($"File '{pxlhm_file}' queued for downloading");

                            string settings_file = Path.ChangeExtension(pxlhm_file, settings_file_ext);
                            if (!File.Exists(settings_file) || !ResultsViewSettings.Load(settings_file, out ResultsViewSettings view_settings))
                                view_settings = new ResultsViewSettings();

                            Data.Add(pxlhm_file, new ResultsViewDataOpt()
                            {
                                InputFile = pxlhm_file,
                                InputData = input_data,
                                SettingsFile = settings_file,
                                Settings = view_settings
                            });
                        }
                //update_data_sleep_token.Cancel();
                //refrash_delay = 0;                
            }).Start();
        }


        public void RemoveFiles(params string[] pxlhm_files)
        {
            new Task(() =>
            {
                foreach (string file in pxlhm_files)
                    lock (Data)
                        if (Data.ContainsKey(file))
                        {
                            Data.Remove(file);
                            MsgHandler?.Invoke($"File '{file}' was removed");
                        }
                //update_data_sleep_token.Cancel();
                //refrash_delay = 0;                
            }).Start();
        }





        private void UpdateTree_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(refrash_delay_normal);
                UpdateTree();
            }
        }

        private void UpdateData_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(refrash_delay_normal);
                UpdateData();
            }
        }

        private void BinDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(refrash_delay_normal);
                BinDownloader();
            }
        }



        Thread UpdateTree_Thread;
        Thread UpdateData_Thread;
        Thread BinDownloader_Thread;


        public async void Start()
        {
            UpdateTree_Thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(refrash_delay_normal);
                    UpdateTree();
                }
            });

            UpdateData_Thread = new Thread(() =>
            {
                while (true)
                {
                    UpdateData();
                    Thread.Sleep(refrash_delay_normal);
                }
            });

            BinDownloader_Thread = new Thread(() =>
            {
                while (true)
                {
                    BinDownloader();
                    Thread.Sleep(refrash_delay_normal);
                }
            });

            UpdateTree_Thread.Start();
            UpdateData_Thread.Start();
            BinDownloader_Thread.Start();
        }


        public void Stop()
        {
            UpdateTree_Thread?.Abort();
            UpdateData_Thread?.Abort();
            BinDownloader_Thread?.Abort();
        }





        public void Start2()
        {
            BackgroundWorker background_tree = new BackgroundWorker();
            BackgroundWorker background_data = new BackgroundWorker();
            BackgroundWorker background_bins = new BackgroundWorker();

            background_tree.DoWork += UpdateTree_DoWork;
            background_data.DoWork += UpdateData_DoWork;
            background_bins.DoWork += BinDownloader_DoWork;

            background_tree.RunWorkerAsync();
            background_data.RunWorkerAsync();
            background_bins.RunWorkerAsync();
        }




        static bool NodeContains(object tag, TreeNodeCollection nodes, out TreeNode result)
        {
            foreach (TreeNode item in CollectNodes(nodes))
                if (TagEquals(item.Tag, tag))
                {
                    result = item;
                    return true;
                }
            result = null;
            return false;
        }



        public const int CS_TAG_KEY = 0;
        public const int ID_TAG_KEY = 1;
        public const int IT_TAG_KEY = 2;


        static bool TagEquals(object o1, object o2)
        {
            var t1 = o1 as Tuple<int, string>;
            var t2 = o2 as Tuple<int, string>;
            return t1 == t2 ||
                   (t1.Item1.Equals(t2.Item1) && t1.Item2.Equals(t2.Item2));
        }


        static IEnumerable<TreeNode> CollectNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;
                foreach (var child in CollectNodes(node.Nodes))
                    yield return child;
            }
        }




        public void UpdateTree()
        {
            TreeView?.BeginInvoke(new Action(() =>
            {
                bool repeat;
                // remove blanked
                repeat = true;
                lock (Data)
                {
                    while (repeat)
                    {
                        repeat = false;
                        foreach (var node in CollectNodes(TreeView.Nodes))
                        {
                            var tag = node.Tag as Tuple<int, string>;
                            if (tag.Item1 == CS_TAG_KEY)
                            {
                                if (!Data.ContainsKey(tag.Item2))
                                {
                                    TreeView.Nodes.Remove(node);
                                    break;
                                }
                            }
                            else if (tag.Item1 == ID_TAG_KEY)
                            {
                                var ptag = node.Parent.Tag as Tuple<int, string>;
                                if (!Data[ptag.Item2].Sets.ContainsKey(tag.Item2))
                                {
                                    TreeView.Nodes.Remove(node);
                                    break;
                                }
                            }
                            else if (tag.Item1 == IT_TAG_KEY)
                            {
                                var ptag = node.Parent.Tag as Tuple<int, string>;
                                var pptag = node.Parent.Parent.Tag as Tuple<int, string>;
                                if (!Data[pptag.Item2].Sets[ptag.Item2].Iterations.ContainsKey(tag.Item2))
                                {
                                    TreeView.Nodes.Remove(node);
                                    break;
                                }
                            }
                        }
                    }
                    // add new
                    var sort_data = Data.Values.OrderBy(x => Path.GetDirectoryName(x.InputFile));  
                    foreach (var data in sort_data)
                    {
                        object cs_tag = Tuple.Create(CS_TAG_KEY, data.InputFile);
                        if (!NodeContains(cs_tag, TreeView.Nodes, out TreeNode id_node))
                        {
                            string id_title = Path.GetFileNameWithoutExtension(Path.GetFileName(data.InputFile));
                            id_node = new TreeNode(id_title) { Tag = cs_tag };
                            TreeView.Nodes.Add(id_node);
                        }
                        foreach (var set in data.Sets)
                        {
                            object id_tag = Tuple.Create(ID_TAG_KEY, set.Key);
                            if (!NodeContains(id_tag, id_node.Nodes, out TreeNode set_node))
                            {
                                string set_title = HistMatching.GetID(set.Key);
                                set_node = new TreeNode(set_title) { Tag = id_tag };
                                id_node.Nodes.Add(set_node);
                            }
                            foreach (var iter in set.Value.Iterations)
                            {
                                object it_tag = Tuple.Create(IT_TAG_KEY, iter.Key);
                                if (!NodeContains(it_tag, set_node.Nodes, out TreeNode iter_node))
                                {
                                    string iter_title = iter.Key;
                                    iter_node = new TreeNode(iter_title) { Tag = it_tag };
                                    set_node.Nodes.Add(iter_node);
                                }
                            }
                        }
                    }
                    // create tree
                    repeat = true;
                    while (repeat)
                    {
                        repeat = false;
                        //var sort_data = Data.Values; //.OrderBy(x => Path.GetDirectoryName(x.InputFile));   
                        foreach (var data in sort_data)
                        {
                            string parent_pxlhm_path = Path.GetDirectoryName(data.InputFile) + "\\" + data.InputData.ParentCase + HistMatching.EXT;
                            if (!string.IsNullOrEmpty(parent_pxlhm_path))
                            {
                                string parent_id_number = data.InputData.ParentID;
                                string parent_iter_number = data.InputData.ParentIter;
                                string id_folder_path = HistMatching.GetIDFolderPath(parent_pxlhm_path, parent_id_number);

                                object id_tag = Tuple.Create(ID_TAG_KEY, id_folder_path);
                                object it_tag = Tuple.Create(IT_TAG_KEY, parent_iter_number);
                                object cs_tag = Tuple.Create(CS_TAG_KEY, data.InputFile);

                                if (NodeContains(id_tag, TreeView.Nodes, out TreeNode id_node) &&
                                    NodeContains(it_tag, id_node.Nodes, out TreeNode it_node) &&
                                    !NodeContains(cs_tag, it_node.Nodes, out _))
                                {
                                    //object cs_tag1 = Tuple.Create(cs_tag_key, data.Value.PXLHMFile);
                                    NodeContains(cs_tag, TreeView.Nodes, out TreeNode reloc_node);
                                    TreeView.Nodes.Remove(reloc_node);
                                    it_node.Nodes.Add(reloc_node);
                                    repeat = true;
                                }
                            }
                        }
                    }
                }
            }));
        }




        List<(string, Task)> Tasks = new List<(string, Task)>();
        void BinDownloader()
        {
            while (Tasks.Count > 0)
            {
                (string, Task) item;
                lock (Tasks) item = Tasks.First();
                item.Item2.Start();
                Task.WaitAll(item.Item2);
                lock (Tasks) Tasks.Remove(item);
            }
        }


        bool TryAddTask(string title, Task task)
        {
            if (Tasks.Select(v => v.Item1).Contains(title))
                return false;
            lock (Tasks) Tasks.Add((title, task));
            return true;
        }





        public void UpdateData()
        {
            AddFiles(PullCases());

            foreach (var data in Data)
            {
                string[] new_id_folders = HistMatching.GetExistingIDFolders(data.Value.InputFile);

                string[] except_folders = data.Value.Sets.Keys.Except(new_id_folders).ToArray();
                foreach (string folder in except_folders)
                {
                    MsgHandler?.Invoke($"Remove '{folder}'");
                    data.Value.Sets.Remove(folder);
                }

                // Input Data
                if (data.Value.InputData.LastWriteTime != File.GetLastWriteTime(data.Value.InputFile))
                {
                    MsgHandler?.Invoke($"Reading '{data.Value.InputFile}' started");
                    HistMatchingInput.Load(data.Value.InputFile, out HistMatchingInput temp);
                    data.Value.InputData = temp;
                    MsgHandler?.Invoke($"Reading '{data.Value.InputFile}' completed");
                }

                // RSMs
                foreach (string folder in new_id_folders)
                {
                    if (!data.Value.Sets.ContainsKey(folder))
                        data.Value.Sets.Add(folder, new IterationSet());

                    IterationSet set = data.Value.Sets[folder];
                    string id = HistMatching.GetID(folder);

                    // GRID
                    string grid_file = folder + "\\" + HistMatching.GetGridFileName(id);
                    if (!File.Exists(grid_file))
                        continue;
                    if (set.Grid.LastWriteTime != File.GetLastWriteTime(grid_file))
                    {
                        TryAddTask(grid_file, new Task(() =>
                        {
                            MsgHandler?.Invoke($"Reading '{grid_file}' started");
                            Grid.ReadBinary(grid_file, out Grid grid);
                            set.Grid = grid;
                            MsgHandler?.Invoke($"Reading '{grid_file}' completed");
                        }));
                    }

                    // ResPress Table
                    string res_press_file = folder + "\\" + HistMatching.GetResPressFileName(id);
                    if (!File.Exists(res_press_file))
                        continue;
                    if (set.ResPressTable.LastWriteTime != File.GetLastWriteTime(res_press_file))
                    {
                        MsgHandler?.Invoke($"Reading '{res_press_file}' started");
                        set.ResPressTable = new DataTable();
                        set.ResPressTable.Read(res_press_file, out _);
                        MsgHandler?.Invoke($"Reading '{res_press_file}' completed");
                    }

                    // init props
                    string set_props_file = HistMatching.GetExistingSetPropsOptFile(folder);
                    if (!File.Exists(set_props_file))
                        continue;
                    if (set.Props.LastWriteTime != File.GetLastWriteTime(set_props_file))
                    {
                        TryAddTask(set_props_file, new Task(() =>
                        {
                            MsgHandler?.Invoke($"Reading '{set_props_file}' started");
                            ActProps.ReadBinary(set_props_file, out ActProps temp);
                            set.Props = temp;
                            MsgHandler?.Invoke($"Reading '{set_props_file}' completed");
                        }));
                    }

                    // iterations
                    string[] rsm_files = HistMatching.GetExistingSumFiles(folder);
                    string[] pxl_files = HistMatching.GetExistingPxlFiles(folder);
                    string[] corey_files = HistMatching.GetExistingCoreyInputFiles(folder);
                    string[] props_files = HistMatching.GetExistingIterPropsOptFiles(folder);

                    string[] except_iters = set.Iterations.Keys.Except(rsm_files.Select(v => HistMatching.GetIter(v))).ToArray();
                    foreach (string iter in except_iters)
                    {
                        MsgHandler?.Invoke($"Remove iter '{iter}' from the folder '{folder}'");
                        set.Iterations.Remove(iter);
                    }

                    // check count before reading!
                    if ((corey_files.Length > 0 && rsm_files.Length != corey_files.Length) ||
                        (props_files.Length > 0 && rsm_files.Length != props_files.Length) ||
                        (pxl_files.Length > 0 && rsm_files.Length != pxl_files.Length))
                    {
                        continue; // TODO: нужно сообщение о проблеме
                    }

                    for (int i = 0; i < rsm_files.Length; ++i)
                    {
                        string iter_title = HistMatching.GetIter(rsm_files[i]);

                        Iteration iteration;
                        if (!set.Iterations.ContainsKey(iter_title))
                            iteration = new Iteration() { Title = iter_title };
                        else
                            iteration = set.Iterations[iter_title];

                        if (iteration.Result.LastWriteTime != File.GetLastWriteTime(rsm_files[i]))
                        {
                            MsgHandler?.Invoke($"Reading '{rsm_files[i]}' started");
                            RSM.Rsm rsm = new RSM.Rsm(rsm_files[i]);
                            iteration.Result = new IterationResult(rsm, out _);
                            MsgHandler?.Invoke($"Reading '{rsm_files[i]}' completed");

                            MsgHandler?.Invoke($"Reading '{pxl_files[i]}' started");
                            HistMatchingInput.Load(pxl_files[i], out HistMatchingInput input);
                            iteration.InputData = input;
                            MsgHandler?.Invoke($"Reading '{pxl_files[i]}' completed");

                            if (corey_files.Length > 0)
                            {
                                MsgHandler?.Invoke($"Reading '{corey_files[i]}' started");
                                CoreySet.Load(corey_files[i], out CoreySet coreySet);
                                iteration.CoreySet = coreySet;
                                MsgHandler?.Invoke($"Reading '{corey_files[i]}' completed");
                            }
                        }

                        if (props_files.Length > 0 &&
                            iteration.Props.LastWriteTime != File.GetLastWriteTime(props_files[i]))
                        {
                            string prop_file = props_files[i];
                            TryAddTask(prop_file, new Task(() =>
                            {
                                MsgHandler?.Invoke($"Reading '{prop_file}' started");
                                ActProps.ReadBinary(prop_file, out ActProps temp);
                                iteration.Props = temp;
                                MsgHandler?.Invoke($"Reading '{prop_file}' completed");
                            }));
                        }

                        if (!set.Iterations.ContainsKey(iter_title))
                            set.Iterations.Add(iter_title, iteration);
                    }
                }
            }
        }



        static string FileName(string id)
        {
            return Application.StartupPath + "\\" + id + ext;
        }


        const string ext = ".downloader";
        static string[] PullCases(string id)
        {
            string filename = FileName(id);
            if (File.Exists(filename))
            {
                try
                {
                    string[] cases = File.ReadAllLines(filename);
                    File.Delete(filename);
                    return cases;
                }
                catch
                {
                    return Array.Empty<string>();
                }
            }
            return Array.Empty<string>();
        }


        public string[] PullCases()
        {
            return PullCases(ID);
        }



        public static void PushCases(string id, params string[] cases)
        {
            List<string> all_cases = new List<string>();
            all_cases.AddRange(PullCases(id));
            all_cases.AddRange(cases);
            try
            {
                File.WriteAllLines(FileName(id), all_cases.ToArray());
            }
            catch { }
        }




    }
}
