/*
 * 
 * 设备厂家：长沙开源
 * 型号：CSKY_5E_KCIV  快速量热仪
 * 转储方式：
 * 
 * */
using HBJYDataCollection.DBHelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 日志类
        /// </summary>
        //public PublicClass.WriteLog writeLog;
        /// <summary>
        /// 文件监视类
        /// </summary>
        public FileSystemWatcher watcher;
        /// <summary>
        /// 时间控件
        /// </summary>
        public System.Timers.Timer timer;
        //声明全局标志位
        bool flag = true;
        /// <summary>
        /// access数据库操作类
        /// </summary>
        private AccessHelper accessHelper;
        /// <summary>
        /// oracle数据库操作类
        /// </summary>
        private OracleHelper oracleHelper;

        #region 控件大小随窗体大小等比例缩放
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }

        #endregion
        public Form1()
        {
            InitializeComponent();
            x = this.Width;
            y = this.Height;
            setTag(this);
        }
        
        private void upload_Click(object sender, EventArgs e)
        {
            if (filepath.Text.Trim() == "")
            {
                MessageBox.Show("请先添加数据库文件路径", "提示！");
            }
            else
            {
                
                if (flag)
                {
                    AddLstMessage("开始存储数据...");
                    string filepath = this.filepath.Text;
                    //监听文件的变化
                    MonFile(filepath);

                    timer = new System.Timers.Timer();
                    timer.Enabled = true;
                    //timer.Enabled = false;
                    timer.Elapsed += new System.Timers.ElapsedEventHandler(tm_Elapsed);

                    watcher.EnableRaisingEvents = true;

                    //存储数据

                }
                else
                {
                    AddLstMessage("关闭存储数据...");
                }
                //标志位反转
                flag = !flag;
            }
            
            
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.filepath.Text = "";
                this.filepath.Text = openFileDialog1.FileName;
            }
        }
        /// <summary>
        /// watcher事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //System.Threading.Thread.Sleep(100);
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Enabled = true;
        }
        /// <summary>
        /// 监视数据库文件的变化
        /// </summary>
        private void MonFile(string filepath)
        {
            try
            {
                watcher = new FileSystemWatcher();
                string[] args = filepath.Trim().Split('\\');
                string sPath = string.Empty;
                for (int i = 0; i < args.Length - 1; i++)
                {
                    sPath += args[i] + "\\";
                }
                watcher.Path = sPath;
                watcher.Filter = args[args.Length - 1];
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += new FileSystemEventHandler(watcher_Changed);
                           
            }catch(Exception ex)
            {
                AddLstMessage("监视文件类错误：" + ex.Message);
                //Console.WriteLine("监视文件类错误：" + ex.Message);
            }
            
        }

        /// <summary>
        /// timer事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                oracleHelper = new OracleHelper("192.168.5.105", "1521","orcl","C##zbl2","zbl2#DB365");
                accessHelper = new AccessHelper(this.filepath.Text, "CSKY");
                timer.Enabled = false;
                //ServiceReference1.ToZDCJWebServiceClient K = new ServiceReference1.ToZDCJWebServiceClient();
                //Console.WriteLine(K.getMaxCollectdateByEquipmentname("Cal01"));
                DataTable dt = new DataTable();

                string sqlString = string.Format(@"SELECT t.Number, t.Mingchen,t.Testtime,t.Qgrad,t.Qgrd,t.Qb FROM win5emdb t 
                                        WHERE t.testtime > #2023/5/22 4:50:46# ORDER BY t.testtime DESC ");
                dt = accessHelper.Query(sqlString);

                if (dt.Rows.Count < 1)
                    return;
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string str = dr["Number"] +"\t"+ dr["Mingchen"] + "\t" + dr["Testtime"] + "\t" + dr["Qgrad"] + "\t" + dr["Qgrd"] + "\t" + dr["Qb"];
                        AddLstMessage("存储的数据为：" + str);
                    }
                }

                //Console.Write("Number" + "\t");
                //Console.Write("Mingchen" + "\t");
                //Console.Write("Testtime" + "\t");
                //Console.Write("Qgrad" + "\t");
                //Console.Write("Qgrd" + "\t");
                //Console.WriteLine("Qb");
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    Console.Write(dt.Rows[i]["Number"].ToString() + "\t");
                //    Console.Write(dt.Rows[i]["Mingchen"].ToString() + "\t");
                //    Console.Write(dt.Rows[i]["Testtime"].ToString() + "\t");
                //    Console.Write(dt.Rows[i]["Qgrad"].ToString() + "\t");
                //    Console.Write(dt.Rows[i]["Qgrd"].ToString() + "\t");
                //    Console.WriteLine(dt.Rows[i]["Qb"].ToString());
                //}

                List<string> sqlStringList = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string equipmentname = "Calo1";
                        //TESTNO - Number
                        string testno = dt.Rows[0]["Number"].ToString();
                        string documentno = dt.Rows[i]["MingChen"].ToString();
                        string itemab = dt.Columns[j].ColumnName;

                        string itemvalue = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                        string collectdate = dt.Rows[i]["testtime"].ToString();
                        string oldcollectdate = dt.Rows[i]["testtime"].ToString();
                        //单位换算   MJ/kg*239.143914391439=Cal/g
                        if (dt.Columns[j].ColumnName == "Qgrad" || dt.Columns[j].ColumnName == "Qgrd")
                        {
                            itemvalue = (Convert.ToDouble(itemvalue)).ToString("0.000");
                        }
                        //itemvalue = (Convert.ToDouble(itemvalue)).ToString("0.000");

                        if (dt.Columns[j].ColumnName == "Qb")//弹筒发热量
                        {
                            itemvalue = (Convert.ToDouble(itemvalue)).ToString("0.000");
                            //如果 testNo 包含 A 说明是 A桶 包含B则是B桶
                            if (testno.IndexOf('A') > 0)
                            {
                                itemvalue = (Convert.ToDouble(itemvalue) * ConfigModel.WriteXS_A).ToString("0.000");
                            }
                            else
                            {
                                itemvalue = (Convert.ToDouble(itemvalue) * ConfigModel.WriteXS_B).ToString("0.000");
                            }
                        }

                        sqlString = string.Format(@"insert into WXQ (
testno,
equipmentname,
documentno,
itemab,
itemvalue,
collectdate,
insertdate,
oldcollectdate) 
values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}')",
testno,
equipmentname,
documentno,
itemab,
itemvalue,
collectdate,
"sysdate",
oldcollectdate);


                        sqlStringList.Add(sqlString);
                        string str = documentno + "," + collectdate + "," + itemab + "," + itemvalue;
                        AddLstMessage("准备转储数据:" + str);
                        //writeLog.WriteMsg("准备转储数据:" + str);
                    }
                }   
                int iTemp = oracleHelper.ExecuteSqlTran(sqlStringList);
                if (iTemp >= 0)
                {
                    AddLstMessage("转储" + Convert.ToString(iTemp) + "条记录");
                }                 
            }
            catch (Exception ex)
            {
                AddLstMessage("timer事件错误:" + ex.Message);
                //writeLog.WriteMsg("timer事件错误:" + ex.Message);
            }
        }
        /// <summary>
        /// 添加滚屏信息
        /// </summary>
        /// <param name="str">要添加的信息</param>
        public void AddLstMessage(string str)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new MethodInvoker(() =>
                {
                    richTextBox1.AppendText(DateTime.Now.ToString() + "---" + str + "\n");
                    richTextBox1.SelectionStart = richTextBox1.Text.Length; //Set the current caret position at the end
                    richTextBox1.ScrollToCaret(); //Now scroll it automatically
                }));
            }
            else
            {
                richTextBox1.AppendText(DateTime.Now.ToString() + "---" + str + "\n");
            }
            //if (this.richTextBox1.Lines.Length >= 10000)
            //{
            //    string[] sLines = richTextBox1.Lines;
            //    string[] sNewLines = new string[sLines.Length - 1];
            //    Array.Copy(sLines, 1, sNewLines, 0, sNewLines.Length);
            //    richTextBox1.Lines = sNewLines;
            //}
            //this.richTextBox1.AppendText(DateTime.Now.ToString() + "---" + str + "\n");
            //this.richTextBox1.ScrollToCaret(); 
        }
    }
}
