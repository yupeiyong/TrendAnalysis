using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 趋势分析
{
    public class WriteAnalysisToDgv<T>
    {
        /// <summary>
        /// 比较维数
        /// </summary>
        private int Weisu;
        /// <summary>
        /// 比较次数
        /// </summary>
        private uint CompareCount;
        /// <summary>
        /// 列表数组
        /// </summary>
        private List<T[,]> lArray = null;
        public WriteAnalysisToDgv(List<T> list,int weisu,uint compareCount)
        {
            this.Weisu = weisu;
            this.CompareCount = compareCount;
            lArray = GetArray(list, this.Weisu, this.CompareCount);
        }
        /// 写入数据到DGV控件（十位、个位四个DGV控件）
        /// </summary>
        /// <param name="lArr">二维数组列表</param>
        /// <param name="dgv">待写入数据的控件</param>
        public void WriteDataToDgv(DataGridView dgv)
        {
            if (lArray == null || lArray.Count == 0) { return; }
            //dgv行号
            int row = -1;
            //遍历列表中每一个字符串二维数组
            foreach (T[,] arr in lArray)
            {
                //取得数组的行数
                int rowCount = arr.GetUpperBound(0) + 1;
                //遍历第一维（行）
                for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
                {
                    row++;
                    dgv.Rows.Add();
                    dgv.Rows[row].Cells[0].Value = (rowCount--).ToString() + " X N";
                    //遍历第二维（列）
                    for (int j = arr.GetLowerBound(1); j <= arr.GetUpperBound(1); j++)
                    {
                        //按行列位置写入数组中的值
                        dgv.Rows[row].Cells[j + 1].Value = arr[i, j].ToString();
                    }
                }
                dgv.Rows[row].Cells[1].Value = "?";
                row++;
                dgv.Rows.Add();
            }
        }
        /// <summary>
        /// 取得排列好的二维数组
        /// 将列表分成比较次数个二维数组，数组列为维数，行数为递增的比较次数
        /// </summary>
        /// <param name="lNum">数字列表</param>
        /// <param name="weisu">维数</param>
        /// <param name="compareCount">比较次数</param>
        /// <returns></returns>
        private List<T[,]> GetArray(List<T> lNum, int weisu, uint compareCount)
        {
            //列表数量不能小于维数*比较次数
            if (lNum.Count < weisu * compareCount)
            {
                throw new Exception("期数小于分析比较次数乘以维数！");
            }
            List<T[,]> lByteArray = new List<T[,]>();
            //按比较次数分割列表
            //如列表：1,2,3,4,5,6,7,8,9,10,11,12,13
            //维数：3（3列）
            //比较次数3
            //最终分成3个二维数组
            //数组1＝{{1,2,3}}一行3列
            //数组2＝{{1,3,5}{2,4,6}}二行3列
            //数组3＝{{1,4,7}{2,5,8}{3,6,9}}三行3列
            /*按数字阵列排列如下：
              公式＝比较次数*当前维数-比较次数（因为遍历时是从0开始所以当前维数和第二个比较次数要+1）              
              1,2,3 
              写入数组：
              array[0,0]=lNum[0]=1
              array[0,1]=lNum[1]=2
              array[0,2]=lNum[2]=3
             写入数组：列表索引计算＝2*(当前维数+1)-(比较次数+1）
              2,4,6
              1,3,5
              array[0,0]=lNum[1]=2
              array[0,1]=lNum[3]=4
              array[0,2]=lNum[5]=6
              array[1,0]=lNum[0]=1
              array[1,1]=lNum[2]=3
              array[1,2]=lNum[4]=5

              3,6,9
              2,5,8
              1,4,7
             
             写入数组：列表索引计算＝3*(当前维数+1)-(比较次数+1）
              array[0,0]=lNum[2]=3
              array[0,1]=lNum[5]=6
              array[0,2]=lNum[8]=9
              array[1,0]=lNum[1]=2
              array[1,1]=lNum[4]=5
              array[1,2]=lNum[7]=8
              array[2,0]=lNum[0]=1
              array[2,1]=lNum[3]=4
              array[2,2]=lNum[6]=7
             */
            for (int c = 1; c <= compareCount; c++)
            {
                T[,] array = new T[c, weisu];
                int curCount = c;
                //比较次数遍历
                for (int i = 0; i < c; i++)
                {
                    //维数遍历
                    for (int w = 0; w < weisu; w++)
                    {
                        //当前比较次数*（当前维数+1）-（比较次数+1）
                        array[i, w] = lNum[curCount * (w + 1) - (i + 1)];
                    }
                }
                lByteArray.Add(array);
            }
            return lByteArray;
        }
    }
}
