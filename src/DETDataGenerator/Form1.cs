using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyBaseConverter;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DETDataGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string sku = txtSku.Text.Trim();
                if (sku.Length == 0)
                {
                    MessageBox.Show("SKU can not be empty!");
                    return;
                }
                ulong startIndex=0;
                uint qty=1;
                if (ulong.TryParse(txtStartIndex.Text, out startIndex) == false)
                {
                    MessageBox.Show("Invalid Start Index!");
                    return;
                }
                if (uint.TryParse(txtQty.Text, out qty) == false)
                {
                    MessageBox.Show("Invalid Qty!");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                string header = "Human Readable,Unique ID, Url, EPC";
                sb.AppendLine(header);
                string csvContent = GenerateSampleData(sku, startIndex, qty);
                sb.AppendLine(csvContent);
                DialogResult dialogResult = saveFileDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    string csvFilename = saveFileDialog1.FileName;
                    using (var sw = File.CreateText(csvFilename))
                    {
                        sw.WriteLine(csvContent);
                        sw.Close();
                    }

                        MessageBox.Show("Data is generated successfully!");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
               
            }
        }
        private  string GenerateSampleData(string sku, ulong startIndex,uint qty,string urlPrefix = "https://y.esquel.cn/b/_13")
        {

            ;
            string skuBase16 = AnyBaseConvert.Convert(sku, AnyBaseConvert.BaseCharSet.Base36_Custom,
                AnyBaseConvert.BaseCharSet.Base16).PadLeft(19, '0');
            StringBuilder sb = new StringBuilder();
            // int startIndex = 1;
            ulong end = startIndex+qty-1;
            for (ulong i = startIndex; i <= end; i++)
            {

                string humanReadable = $"{sku}{i.ToString().PadLeft(7, '0')}";
                var hashCode = Math.Abs(humanReadable.GetHashCode());
                string sequenceBase16 = AnyBaseConvert
                    .Convert(i.ToString(), AnyBaseConvert.BaseCharSet.Base10, AnyBaseConvert.BaseCharSet.Base16)
                    .PadLeft(13, '0');
                string epcBase16 = $"{skuBase16}{sequenceBase16}";
                var randomedHumanReadable = $"{hashCode.ToString().First()}{humanReadable}";

                var uid = AnyBaseConvert.Convert(randomedHumanReadable, AnyBaseConvert.BaseCharSet.Base36_Custom,
                    AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = AnyBaseConvert.Convert(uid, AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom,
                    AnyBaseConvert.BaseCharSet.Base36_Custom);
                if (randomedHumanReadable != uid2)
                {
                    int x = 1;
                }

                //Humanread, Unique Id,Url

                // string urlPrefix = "https://y.esquel.cn/b/_13";
                sb.AppendLine($"{humanReadable},{uid},{urlPrefix}{uid},{epcBase16}");
            }

            string result = sb.ToString();
            return result;
        }
  private  string GenerateSampleDataV2(OrderInfo orderItem,string sku, ulong startIndex,uint qty,string urlPrefix = "https://y.esquel.cn/b/_13")
        {

            ;
            string skuBase16 = AnyBaseConvert.Convert(sku, AnyBaseConvert.BaseCharSet.Base36_Custom,
                AnyBaseConvert.BaseCharSet.Base16).PadLeft(19, '0');
            StringBuilder sb = new StringBuilder();
            // int startIndex = 1;
            ulong end = startIndex+qty-1;
            for (ulong i = startIndex; i <= end; i++)
            {

                string humanReadable = $"{sku}{i.ToString().PadLeft(7, '0')}";
                var hashCode = Math.Abs(humanReadable.GetHashCode());
                string sequenceBase16 = AnyBaseConvert
                    .Convert(i.ToString(), AnyBaseConvert.BaseCharSet.Base10, AnyBaseConvert.BaseCharSet.Base16)
                    .PadLeft(13, '0');
                string epcBase16 = $"{skuBase16}{sequenceBase16}";
                var randomedHumanReadable = $"{hashCode.ToString().First()}{humanReadable}";

                var uid = AnyBaseConvert.Convert(randomedHumanReadable, AnyBaseConvert.BaseCharSet.Base36_Custom,
                    AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = AnyBaseConvert.Convert(uid, AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom,
                    AnyBaseConvert.BaseCharSet.Base36_Custom);
                if (randomedHumanReadable != uid2)
                {
                    int x = 1;
                }

                //Humanread, Unique Id,Url

                // string urlPrefix = "https://y.esquel.cn/b/_13";
                sb.AppendLine($"{orderItem.GO},{orderItem.ProductCode},{orderItem.Sku},{orderItem.Style},{orderItem.Size},{orderItem.Color},{humanReadable},{uid},{urlPrefix}{uid},{epcBase16}");
            }

            string result = sb.ToString();
            return result;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtOrderFileName.Text = openFileDialog1.FileName;
            }
        }

        public class OrderIndex
        {
            public string Sku { get; set; }
            public int  Sequence { get; set; }
        }
        private void btnBatchGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                List<OrderInfo> orderItems = new List<OrderInfo>();
                ISheet sheet;
                using (var stream = new FileStream(txtOrderFileName.Text, FileMode.Open))
                {
                    stream.Position = 0;
                    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                    sheet = xssWorkbook.GetSheetAt(0);
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    /*for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        {
                            dtTable.Columns.Add(cell.ToString());
                        }
                    }*/

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        OrderInfo item = new OrderInfo();
                        item.GO = row.GetCell(0).ToString();
                        item.ProductCode = row.GetCell(1).ToString();
                        item.Sku = row.GetCell(2).ToString();
                        item.Style = row.GetCell(7).ToString();//H
                        item.Size = row.GetCell(9).ToString();//J
                        item.Color = row.GetCell(11).ToString();//L
                        item.Qty = Convert.ToInt32(row.GetCell(21).ToString());//L
                        orderItems.Add(item);
                       
                    }

                    List<SkuIndex> skuIndices = GetSkuIndicesFromHistory();
                    Dictionary<string, ulong> Index = skuIndices.GroupBy(c => c.Sku).Select(g=>new
                    {
                        Sku=g.Key,
                        Index = g.Max(row=>row.EndIndex)+1
                    }).ToDictionary(x=>x.Sku,x=>x.Index);
                    StringBuilder sb = new StringBuilder();
                    string header = "GO,Product Code,SKU,Style,Size,Color,Human Readable,Unique ID, Url, EPC";
                    sb.AppendLine(header);
                    foreach (OrderInfo orderItem in orderItems)
                    {

                        ulong startIndex = 1;
                        // if(skuIndices.Any(c=>c.Sku==orderItem.Sku))
                        if (Index.ContainsKey(orderItem.Sku))
                        {
                            startIndex = Index[orderItem.Sku];
                        }

                        uint orderItemQty = (uint) orderItem.Qty;
                        string csv = GenerateSampleDataV2(orderItem,orderItem.Sku, startIndex, orderItemQty);
                        sb.Append(csv);

                        ulong itemQty = startIndex + orderItemQty - 1;
                        if (Index.ContainsKey(orderItem.Sku))
                        {
                            Index[orderItem.Sku] = itemQty;
                        }
                        else
                        {
                            Index.Add(orderItem.Sku, itemQty);
                        }
                    }

                    DialogResult dialogResult = saveFileDialog1.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        string csvFilename = saveFileDialog1.FileName;
                        using (var sw = File.CreateText(csvFilename))
                        {
                            sw.WriteLine(sb.ToString());
                            sw.Close();
                        }

                        MessageBox.Show("Data is generated successfully!");
                    }
                    //Save back to index file
                    foreach (KeyValuePair<string, ulong> keyValuePair in Index)
                    {
                        SkuIndex newSkuIndex= new SkuIndex()
                        {
                            CreateDateTime = DateTime.Now,
                            EndIndex = keyValuePair.Value,
                            Sku = keyValuePair.Key
                        };
                        skuIndices.Add(newSkuIndex);
                    }

                    SaveSkuGenerateHistory(Index);
                    //Process data
                    int a = 1;
                }
}
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        string indexFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Order-GenerationHistory.json");
        private void SaveSkuGenerateHistory(Dictionary<string, ulong> index)
        {
            var existingHistory = GetSkuIndicesFromHistory();
            foreach (KeyValuePair<string, ulong> keyValuePair in index)
            {
                SkuIndex newIndex = new SkuIndex()
                {
                    Sku = keyValuePair.Key,
                    EndIndex = keyValuePair.Value,
                    CreateDateTime = DateTime.Now
                };

                existingHistory.Add(newIndex);
            }

            File.WriteAllText(indexFilename, JsonConvert.SerializeObject(existingHistory));

        }

        private List<SkuIndex> GetSkuIndicesFromHistory()
        {
            List<SkuIndex> indices = new List<SkuIndex>();
            if (File.Exists(indexFilename))
            {
                indices = JsonConvert.DeserializeObject<List<SkuIndex>>(File.ReadAllText(indexFilename));
            }
            else
            {
                string parentDirectoryName = Path.GetDirectoryName(indexFilename);
                if (Directory.Exists(parentDirectoryName) == false)
                {
                    Directory.CreateDirectory(parentDirectoryName);
                }
                //Create index file
                using (var sw = File.CreateText(indexFilename))
                {
                    sw.Write(JsonConvert.SerializeObject(indices));
                    sw.Close();
                }
            }

            return indices;
        }
    }
}
