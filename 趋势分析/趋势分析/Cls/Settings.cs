using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 趋势分析
{
    /// <summary>
    /// 设置对象
    /// </summary>
    class Settings
    {
        private string _oddMark = "1";
        /// <summary>
        /// 奇数标志
        /// </summary>
        public string OddMark
        {
            get { return _oddMark; }
            set { _oddMark = value; }
        }
        private string _evenMark = "0";
        /// <summary>
        /// 偶数标志
        /// </summary>
        public string EvenMark
        {
            get { return _evenMark; }
            set { _evenMark = value; }
        }
        private string _bigMark = "1";
        /// <summary>
        /// 大数标志
        /// </summary>
        public string BigMark
        {
            get { return _bigMark; }
            set { _bigMark = value; }
        }
        private string _smallMark = "0";
        /// <summary>
        /// 小数标志
        /// </summary>
        public string SmallMark
        {
            get { return _smallMark; }
            set { _smallMark = value; }
        }
        private Color _odd_color = Color.Red;
        /// <summary>
        /// 奇数颜色
        /// </summary>
        public Color ODD_COLOR
        {
            get { return _odd_color; }
            set { _odd_color = value; }
        }

        private Color _even_color = Color.Blue;
        /// <summary>
        /// 偶数颜色
        /// </summary>
        public Color EVEN_COLOR
        {
            get { return _even_color; }
            set { _even_color = value; }
        }
        private Color _big_color = Color.Black;
        /// <summary>
        /// 大数颜色
        /// </summary>
        public Color BIG_COLOR
        {
            get { return _big_color; }
            set { _big_color = value; }
        }
        private Color _small_color = Color.Green;
        /// <summary>
        /// 大数颜色
        /// </summary>
        public Color SMALL_COLOR
        {
            get { return _small_color; }
            set { _small_color = value; }
        }
        private Color _continue_cell_color = Color.LightCyan;
        /// <summary>
        /// 连续单元格颜色
        /// </summary>
        public Color CONTINUE_CELL_COLOR
        {
            get { return _continue_cell_color; }
            set { _continue_cell_color = value; }
        }
        private Color _same_cell_color = Color.Lavender;
        /// <summary>
        /// 一致单元格颜色
        /// </summary>
        public Color SAME_CELL_COLOR
        {
            get { return _same_cell_color; }
            set { _same_cell_color = value; }
        }

        private Color _same_cell_another_color=Color.YellowGreen;
        /// <summary>
        /// 一致单元格的另一种颜色，如一致单元格第一个单元格为小数，此行第一个单元格为大数，就应当设置不同的颜色
        /// </summary>
        public Color SAME_CELL_ANOTHER_COLOR
        {
            get
            {
                return _same_cell_another_color;
            }
            set
            {
                _same_cell_another_color = value;
            }
        }
        private uint _timeCount = 500;
        /// <summary>
        /// 显示的期数
        /// </summary>
        public uint TimeCount
        {
            get { return _timeCount; }
            set { _timeCount = value; }
        }
        private uint _compareCount = 15;
        /// <summary>
        /// 比较次数
        /// </summary>
        public uint CompareCount
        {
            get { return _compareCount; }
            set { _compareCount = value; }
        }
        private byte _weisu = 8;
        /// <summary>
        /// 维数
        /// </summary>
        public byte Weisu
        {
            get { return _weisu; }
            set { _weisu = value; }
        }
        private byte _tenBigSmallCompBase = 2;
        /// <summary>
        /// 十位大小比较基数
        /// </summary>
        public byte TenBigSmallCompBase
        {
            get { return _tenBigSmallCompBase; }
            set { _tenBigSmallCompBase = value; }
        }
        private byte _unitsBigSmallCompBase = 5;
        /// <summary>
        /// 个位大小比较基数
        /// </summary>
        public byte UnitsBigSmallCompBase
        {
            get { return _unitsBigSmallCompBase; }
            set { _unitsBigSmallCompBase = value; }
        }
        private byte _continueCount = 3;
        /// <summary>
        /// 连续单元格位数
        /// </summary>
        public byte ContinueCount
        {
            get { return _continueCount; }
            set { _continueCount = value; }
        }
        private byte _sameCount = 3;
        /// <summary>
        /// 一致单元格位数
        /// </summary>
        public byte SameCount
        {
            get { return _sameCount; }
            set { _sameCount = value; }
        }
        private uint _continueRowsCount = 3;
        /// <summary>
        /// 连续行数，大于等于此数才显示指定的背景颜色
        /// </summary>
        public uint ContinueRowsCount
        {
            get { return _continueRowsCount; }
            set { _continueRowsCount = value; }
        }
        private uint _sameRowsCount = 3;
        /// <summary>
        /// 一致行数，大于等于此数才显示指定的背景颜色
        /// </summary>
        public uint SameRowCount
        {
            get { return _sameRowsCount; }
            set { _sameRowsCount = value; }
        }
        /// <summary>
        /// 分析记录的类型
        /// 一按数字的位数从个位到十位百位等
        /// </summary>
        public string AnalysisRecordType = string.Empty;
        public Settings()
        {
            GetConfiguration();
        }
        /// <summary>
        /// 获取配置参数并设置控件
        /// </summary>
        private void GetConfiguration()
        {
            //设置控件背景颜色
            Color c = new Color();
            //获取各配置颜色项
            if (TryGetColor("ODD_COLOR", out c))
            {
                _odd_color = c;
            }
            if (TryGetColor("EVEN_COLOR", out c))
            {
                _even_color = c;
            }
            if (TryGetColor("BIG_COLOR", out c))
            {
                _big_color = c;
            }
            if (TryGetColor("SMALL_COLOR", out c))
            {
                _small_color = c;                
            }
            if (TryGetColor("CONTINUE_CELL_COLOR", out c))
            {
                _continue_cell_color = c;
            }
            if (TryGetColor("SAME_CELL_COLOR", out c))
            {
                _same_cell_color = c;
            }
            if (TryGetColor("SAME_CELL_ANOTHER_COLOR", out c))
            {
                _same_cell_another_color = c;
            }
            string value;
            //获取各标志字符
            if (TryGetString("OddMark", out value))
            {
                _oddMark = value;
            }
            if (TryGetString("EvenMark", out value))
            {
                _evenMark = value;
            }
            if (TryGetString("BigMark", out value))
            {
                _bigMark = value;
            }
            if (TryGetString("SmallMark", out value))
            {
                _smallMark = value;
            }
            if (TryGetString("analysisRecordType", out value))
            {
                AnalysisRecordType = value;
            }
            byte b;
            if (TryGetByte("CompareCount", out b))
            {
                _compareCount = b;
            }
            if (TryGetByte("Weisu", out b))
            {
                _weisu = b;
            }
            if (TryGetByte("TenBigSmallCompBase", out b))
            {
                _tenBigSmallCompBase = b;
            }
            if (TryGetByte("UnitsBigSmallCompBase", out b))
            {
                _unitsBigSmallCompBase = b;
            }
            if (TryGetByte("ContinueCount", out b))
            {
                _continueCount = b;
            }
            if (TryGetByte("SameCount", out b))
            {
                _sameCount = b;
            }
            uint timeCount;
            if (TryGetUint("TimeCount", out timeCount))
            {
                _timeCount = timeCount;
            }
            uint rowCount;
            if (TryGetUint("ContinueRowsCount", out rowCount))
            {
                _continueRowsCount = rowCount;                
            }
            if (TryGetUint("SameRowCount", out rowCount))
            {
                _sameRowsCount = rowCount;                
            }           
        }

        private bool TryGetColor(string setting, out Color color)
        {
            //取得设置颜色值
            object obj = ConfigurationManager.AppSettings[setting];
            color = new Color();
            if (obj == null) { return false; }
            string value = obj.ToString();
            int colorValue;
            if (int.TryParse(value, out colorValue))
            {
                color = Color.FromArgb(colorValue);
                return true;
            }
            else
            {                
                return false;
            }
        }

        private bool TryGetString(string setting,out string value)
        {
            //取得设置值
            object obj = ConfigurationManager.AppSettings[setting];
            value = string.Empty;
            if (obj == null) { return false; }
            value = obj.ToString();
            if (value.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryGetByte(string setting, out byte value)
        {
            //取得设置值
            object obj = ConfigurationManager.AppSettings[setting];
            value = 0;
            if (obj == null) { return false; }
            string str = obj.ToString();
            if (byte.TryParse(str, out value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryGetUint(string setting, out uint value)
        {
            //取得设置值
            object obj = ConfigurationManager.AppSettings[setting];
            value = 0;
            if (obj == null) { return false; }
            string str = obj.ToString();
            if (uint.TryParse(str, out value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        public void SaveConfig()
        {
            try
            {
                string appFileName = Application.ExecutablePath + ".config";
                XmlDocument doc = new XmlDocument();
                doc.Load(appFileName);
                XmlNodeList appNodeList = doc.GetElementsByTagName("appSettings");
                if (appNodeList.Count == 0)
                {
                    XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "appSettings", null);
                    XmlNodeList configNodeList = doc.GetElementsByTagName("configuration");
                    configNodeList[0].AppendChild(newNode);
                    appNodeList = doc.GetElementsByTagName("appSettings");
                }
                XmlNode appNode = appNodeList[0];
                //取得当前对象类型
                Type type = this.GetType();
                //取得所有属性
                System.Reflection.PropertyInfo[] arr = type.GetProperties();
                foreach (System.Reflection.PropertyInfo info in arr)
                {
                    string propertyName = info.Name;
                    object value = info.GetValue(this, null);
                    if (value.GetType() == typeof(Color))
                    {
                        Color c = (Color)value;
                        value = c.ToArgb();
                    }
                    bool isExist = false;
                    foreach (XmlNode node in appNode.ChildNodes)
                    {
                        if (node.Name == "add")
                        {
                            if (node.Attributes["key"].Value == propertyName)
                            {
                                node.Attributes["value"].Value = value.ToString();
                                isExist = true;
                            }
                        }
                    }
                    if (isExist == false)
                    {
                        //添加新的元素节点
                        XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "add", null);
                        //添加新的属性节点
                        XmlAttribute att = doc.CreateAttribute("key");
                        //属性值
                        att.Value = propertyName;
                        //新节点添加属性节点
                        newNode.Attributes.SetNamedItem(att);
                        att = doc.CreateAttribute("value");
                        att.Value = value.ToString();
                        //新节点添加属性节点
                        newNode.Attributes.SetNamedItem(att);
                        //添加此新节点
                        appNode.AppendChild(newNode);
                    }
                }
                doc.Save(appFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置文件发生错误，" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
