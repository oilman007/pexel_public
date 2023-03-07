using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    public class Prop
    {
        public Prop()
        {
            Clear();
        }


        public Prop(Prop other)
        {
            Copy(other);
        }


        public Prop(int nx, int ny, int nz, double value, string title = null)
        {
            Clear();
            if (title != null) Title = title;
            Values = new double[nx, ny, nz];
            //double randomValue = 0f;
            //Random rand = new Random();

            /*
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        Values[i, j, k] = value;
                        //Values[i, j, k] = rand.Nextdouble();
                        //Values[i, j, k] = randomValue++;
                    }
            */

            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        Values[i, j, k] = value;
                        //Values[i, j, k] = rand.Nextdouble();
                        //Values[i, j, k] = randomValue++;
                    }
            });
            UpdateScale();
        }


        public Prop(int nx, int ny, int nz, double min, double max, string title = null)
        {
            Clear();
            if (title != null) Title = title;
            Values = new double[nx, ny, nz];
            Random rand = new Random();
            double mult = max - min;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = mult * rand.NextDouble() + min;
            UpdateScale();
        }


        public Prop(int nx, int ny, int nz, string kw, string file, FileType type)
        {
            Read(nx, ny, nz, kw, file, type);
        }


        public Prop(string title, double[,,] values, List<ModifiersGroup> mg, PropScale scale)
        {
            this.Title = title;
            this.Values = values;
            this.Groups = mg;
            this.Scale = scale;
        }






        public DateTime LastWriteTime { set; get; }

        public const double DefaultValue = -999;
        public double[,,] Values { set; get; }
        public string Title { set; get; }
        public PropScale Scale { set; get; }
        public List<ModifiersGroup> Groups { set; get; }
        public  ModifiersGroup CurrModifiersGroup { set; get; }

        static long Namber = 1;



        public double Value(Index3D index)
        {
            if (index is null)
                return DefaultValue;
            return Values[index.I, index.J, index.K];
        }


        public void Copy(Prop p)
        {
            Clear();
            Title = p.Title;
            Scale = new PropScale(p.Scale.Min, p.Scale.Max, p.Scale.Auto);
            int nx = p.NX(), ny = p.NY(), nz = p.NZ();
            Values = new double[nx, ny, nz];

            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = p.Values[i, j, k];
            });

            foreach (ModifiersGroup mg in p.Groups)
            {
                int count = mg.Modifiers.Length;
                Modifier[] ms = new Modifier[count];
                for (int i = 0; i < count; ++i) ms[i] = mg.Modifiers[i];
                Groups.Add(new ModifiersGroup(mg.Title, ms, mg.Applied));
            }
        }







        public void Write(BinaryWriter writer)
        {
            writer.Write(Title);
            int nx = NX(), ny = NY(), nz = NZ();
            writer.Write(nx);
            writer.Write(ny);
            writer.Write(nz);
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                    for (int k = 0; k < nz; ++k)
                        writer.Write(Values[i, j, k]);
            writer.Write(Groups.Count);
            foreach(ModifiersGroup m in Groups)
                m.Write(writer);
            this.Scale.Write(writer);
        }




        public static Prop Read(BinaryReader reader)
        {
            string title = reader.ReadString();
            int nx = reader.ReadInt32();
            int ny = reader.ReadInt32();
            int nz = reader.ReadInt32();
            double[,,] values = new double[nx, ny, nz];
            for (int i = 0; i < nx; ++i)
                for (int j = 0; j < ny; ++j)
                    for (int k = 0; k < nz; ++k)
                        values[i, j, k] = reader.ReadDouble();
            int nm = reader.ReadInt32();
            List<ModifiersGroup> mg = new List<ModifiersGroup>();
            for (int m = 0; m < nm; ++m)
                mg.Add(ModifiersGroup.Read(reader));
            PropScale scale = PropScale.Read(reader);
            return new Prop(title, values, mg, scale);
        }



        



        public void UpdateScale()
        {
            this.Scale.Min = Min();
            this.Scale.Max = Max();
            //this.Scale.Histogram = Histogram(this.Scale.Step);
        }






        void Clear()
        {
            Values = new double[0, 0, 0];
            Title = "Prop_" + Helper.ShowLong(Namber++);
            Scale = new PropScale();
            Groups = new List<ModifiersGroup>();
        }

        


        public int NX()
        {
            return Values.GetLength(0);
        }
        public int NY()
        {
            return Values.GetLength(1);
        }
        public int NZ()
        {
            return Values.GetLength(2);
        }

        public double Min()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (nx == 0 || ny == 0 || nz == 0)
                return DefaultValue;
            double r = Values[0, 0, 0];
            Parallel.For(0, nz, k =>
            {
                //for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (r > Values[i, j, k])
                            r = Values[i, j, k];
            });
            return r;
        }

        public double Max()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (nx == 0 || ny == 0 || nz == 0)
                return DefaultValue;
            double r = Values[0, 0, 0];
            Parallel.For(0, nz, k =>
            {
                //for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (r < Values[i, j, k])
                            r = Values[i, j, k];
            });
            return r;
        }

        public double Aver()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (nx == 0 || ny == 0 || nz == 0)
                return DefaultValue;
            // if (min == max) retrun min;
            double r = 0, c = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (DefaultValue != Values[i, j, k])
                        {
                            r += Values[i, j, k];
                            ++c;
                        }
            return c > 0 ? r / c : 0;
        }


        public double[] Histogram(int n)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; ++i) result[i] = 0;
            double min = Min(), max = Max(), step = (max - min) / (n - 1);
            if(step == 0)
                return result;
            int nx = NX(), ny = NY(), nz = NZ();
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                    {
                        if (Values[i, j, k] == DefaultValue) continue;
                        double p = (Values[i, j, k] - min) / step;
                        result[(int)p] += 1;
                    }
            if (result.Length > 0)
            {
                double max_value = result[0];
                for (int i = 0; i < n; ++i) if (max_value < result[i]) max_value = result[i];
                for (int i = 0; i < n; ++i) result[i] /= max_value;
            }
            return result;
        }



        public bool Read(int nx, int ny, int nz, string kw, string file, FileType type)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return ReadGRDECL(nx, ny, nz, kw, file);
                case FileType.CMG_ASCII:
                    return ReadCMG(nx, ny, nz, kw, file);
                default:
                    return false;
            }
        }


        bool ReadGRDECL(int nx, int ny, int nz, string kw, string file)
        {
            Clear();
            this.LastWriteTime = File.GetLastWriteTime(file);
            Title = kw;
            FileIO.EclFile eclfile = new FileIO.EclFile(file);
            if (!eclfile.GetSingleValuesArray(kw, out double[] values))
                return false;
            if (values.Length != nx * ny * nz)
                return false;
            Values = new double[nx, ny, nz];
            int n = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] = values[n++];
            UpdateScale();
            return true;
        }


        bool ReadCMG(int nx, int ny, int nz, string kw, string file)
        {
            Clear();
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    Title = kw;
                    Values = new double[nx, ny, nz];
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        if (FileIO.CMGReader.ClearLine(line) == kw)
                            break;
                    List<string> values = new List<string>();
                    int valuesNeeded = nx;
                    int n = 0;
                    for (int k = 0; k < nz; ++k)
                    {
                        for (int j = 0; j < ny; ++j)
                        {
                            while (values.Count < valuesNeeded && (line = sr.ReadLine()) != null)
                            {
                                line = FileIO.CMGReader.ClearLine(line);
                                if (!string.IsNullOrEmpty(line))
                                    foreach (string word in line.Split())
                                        values.Add(word);
                            }
                            for (int i = 0; i < nx; ++i)
                            {
                                Values[i, j, k] = Helper.ParseDouble(values[n++]);
                            }
                            values.RemoveRange(0, valuesNeeded);
                            n = 0;
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                Clear();
                return false;
            }
        }

        
        public bool Write(string kw, string file, FileType type, params string[] comments)
        {
            switch (type)
            {
                case FileType.GRDECL_ASCII:
                    return WriteGRDECL(kw, file, comments);
                case FileType.CMG_ASCII:
                    return WriteCMG(kw, file);
                default:
                    return false;
            }
        }


        /*
        bool WriteGRDECL(string kw, string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(kw);
                    string line = string.Empty;
                    int c = 0;
                    int nx = NX(), ny = NY(), nz = NZ();
                    for (int k = 0; k < nz; ++k)
                        for (int j = 0; j < ny; ++j)
                            for (int i = 0; i < nx; ++i)
                            {
                                line += OutputStringFormat(Values[i, j, k]);
                                if (++c == nColumn)
                                {
                                    sw.WriteLine(line);
                                    line = string.Empty;
                                    c = 0;
                                }
                            }
                    if (line != string.Empty)
                        sw.WriteLine(line);
                    sw.WriteLine("/");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        */




        public void Bound(double min, double max)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (Values[i, j, k] < min) Values[i, j, k] = min;
                        else
                        if (Values[i, j, k] > max) Values[i, j, k] = max;
        }

        public void Bound(Prop min, Prop max)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (min is null || max is null ||
                nx != min.NX() || ny != min.NY() || nz != min.NZ() ||
                nx != max.NX() || ny != max.NY() || nz != max.NZ())
                return;
            /*
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (Values[i, j, k] < min.Values[i, j, k]) Values[i, j, k] = min.Values[i, j, k];
                        else
                        if (Values[i, j, k] > max.Values[i, j, k]) Values[i, j, k] = max.Values[i, j, k];
            */
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (Values[i, j, k] < min.Values[i, j, k]) Values[i, j, k] = min.Values[i, j, k];
                        else
                        if (Values[i, j, k] > max.Values[i, j, k]) Values[i, j, k] = max.Values[i, j, k];
            });
        }


        bool WriteGRDECL(string kw, string file, params string[] comments)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            double[] values = new double[nx * ny * nz];
            int n = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        values[n++] = Values[i, j, k];

            string[] def_comments = new string[2];
            def_comments[0] = "Grid (NXxNYxNZ)  : " +  Helper.ShowInt(nx) + "x" + Helper.ShowInt(ny) + "x" + Helper.ShowInt(nz);
            def_comments[1] = "Min - Aver - Max : " + Helper.ShowDouble(Min()) + " - " + Helper.ShowDouble(Aver()) + " - " + Helper.ShowDouble(Max());
            /*
            def_comments[1] = nx.ToString() + "x" + ny.ToString() + "x" + nz.ToString() + 
                                " | " + Min().ToString() + " - " + Aver().ToString() + " - " + Max().ToString();
            */
            string[] result_comments = new string[comments.Length + 1 + def_comments.Length];
            comments.CopyTo(result_comments, 0);
            def_comments.CopyTo(result_comments, comments.Length + 1);

            return FileIO.EclFile.Write(kw, file, values, false, result_comments);
        }

        bool WriteCMG(string kw, string file)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine("*" + kw + " *ALL");
                    string line = string.Empty;
                    int c = 0;
                    int nx = NX(), ny = NY(), nz = NZ();
                    for (int k = 0; k < nz; ++k)
                        for (int j = 0; j < ny; ++j)
                            for (int i = 0; i < nx; ++i)
                            {
                                line += OutputStringFormat(Values[i, j, k]);
                                if (++c == nColumn)
                                {
                                    sw.WriteLine(line);
                                    line = string.Empty;
                                    c = 0;
                                }
                            }
                    if (!string.IsNullOrEmpty(line))
                        sw.WriteLine(line);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        const int nColumn = 4;

        string OutputStringFormat(double value)
        {
            return " " + Helper.ShowDouble(value);
            //return " " + string.Format("{0,18}", value);
        }









        /*
            Parallel.For(0, ny, j =>
            {
            });
        */




        public void Apply(Grid grid, int index)
        {
            ModifiersGroup mgroup = this.Groups[index];
            int dz = mgroup.Modifiers.Length;
            mgroup.Applied = !mgroup.Applied;
            //for (int k = 0; k < dz; ++k)
            Parallel.For(0, dz, k =>     // здесь parallel работает хорошо
            {
                if (mgroup.Modifiers[k].Use)
                    this.Apply(grid, mgroup.Modifiers[k], mgroup.Applied, false);
            });
            if (this.Scale.Auto) UpdateScale();
        }



        public void Apply(Grid grid, Modifier modifier, bool apply, bool updatesclae = true)
        {
            double c = (modifier.Value - 1) / modifier.Radius;
            int NX = grid.NX(), NY = grid.NY(), NZ = grid.NZ();
            double x = modifier.Location.X;
            double y = modifier.Location.Y;
            double r = modifier.Radius;
            int layer = modifier.Layer;
            //double xl = x - r, xu = x + r, yl = y - r, yu = y + r;
            //Parallel.For(0, NX, i =>              // здесь parallel не работает
            for (int i = 0; i < NX; ++i)
            {
                for (int j = 0; j < NY; ++j)
                {
                    Point3D center = grid.LocalCell(i, j, layer).MiddleTopFace.Center();
                    //if (xl < center.X && center.X < xu && yl < center.Y && center.Y < yu)                    
                    double dx = x - center.X;
                    double dy = y - center.Y;
                    double d = Math.Sqrt(dx * dx + dy * dy);
                    if (d <= r)
                    {
                        double value = (c * (r - d) + 1);
                        if (apply) this.Values[i, j, layer] *= value;
                        else this.Values[i, j, layer] /= value;
                    }
                }
            }//);
            if (updatesclae && this.Scale.Auto) UpdateScale();
        }



        /*
        public void Apply(Grid grid, int index)
        {
            Modifier modifier = this.Modifiers[index];
            double c = (modifier.Value - 1) / modifier.Radius;
            int NX = grid.NX(), NY = grid.NY(), NZ = grid.NZ();
            if (!modifier.Applied)
            {
                for (int i = 0; i < NX; ++i)
                    for (int j = 0; j < NY; ++j)
                    {
                        Point3D center = grid.LocalCell(i, j, 0).MiddleTopFace().Center();
                        double x = modifier.Location.X - center.X;
                        double y = modifier.Location.Y - center.Y;
                        double d = Math.Pow(x * x + y * y, 0.5);
                        if (d <= modifier.Radius)
                            foreach (int k in modifier.Layers)
                                this.Values[i, j, k] *= (c * (modifier.Radius - d) + 1);
                    }
            }
            else
            {
                for (int i = 0; i < NX; ++i)
                    for (int j = 0; j < NY; ++j)
                    {
                        Point3D center = grid.LocalCell(i, j, 0).MiddleTopFace().Center();
                        double x = modifier.Location.X - center.X;
                        double y = modifier.Location.Y - center.Y;
                        double d = Math.Pow(x * x + y * y, 0.5);
                        if (d <= modifier.Radius)
                            foreach (int k in modifier.Layers)
                                this.Values[i, j, k] /= (c * (modifier.Radius - d) + 1);
                    }
            }
            modifier.Applied = !modifier.Applied;
        }
        */



        public void AddAndApply(Grid grid, ModifiersGroup m)
        {
            this.Groups.Add(m);
            Apply(grid, Groups.Count - 1);
        }
        



        public bool Multiplied(Prop other)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            if (other.NX() != nx || other.NY() != ny || other.NZ() != nz)
                return false;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        Values[i, j, k] *= other.Values[i, j, k];
            UpdateScale();
            return true;
        }









        public double[] ValuesArray()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            double[] result = new double[nx * ny * nz];
            int n = 0;
            for (int k = 0; k < nz; ++k)
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        result[n++] = Values[i, j, k];
            return result;
        }



        public bool [,,] IsEquals(double value)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            bool[,,] result = new bool[nx, ny, nz];
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        result[i, j, k] = (Values[i, j, k] == value);
            });
            return result;
        }


        public bool[,,] IsNotEquals(double value)
        {
            int nx = NX(), ny = NY(), nz = NZ();
            bool[,,] result = new bool[nx, ny, nz];
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        result[i, j, k] = (Values[i, j, k] != value);
            });
            return result;
        }


        public int[,,] GetInt()
        {
            int nx = NX(), ny = NY(), nz = NZ();
            int[,,] result = new int[nx, ny, nz];
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        result[i, j, k] = (int)Values[i, j, k];
            });
            return result;
        }




        public double[] UniqValues()
        {
            List<double> result = new List<double>();
            int nx = NX(), ny = NY(), nz = NZ();
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (!result.Contains(Values[i, j, k]))
                            lock (result)
                                result.Add(Values[i, j, k]);
            });
            result.Sort();
            return result.ToArray();
        }





        public Index3D[] Indecies(double value)
        {
            List<Index3D> result = new List<Index3D>();
            int nx = NX(), ny = NY(), nz = NZ();
            Parallel.For(0, nz, k =>
            {
                for (int j = 0; j < ny; ++j)
                    for (int i = 0; i < nx; ++i)
                        if (Values[i, j, k] == value)
                            lock (result)
                                result.Add(new Index3D(i, j, k));
            });
            // TODO: sorting
            return result.ToArray();
        }






    }
}
