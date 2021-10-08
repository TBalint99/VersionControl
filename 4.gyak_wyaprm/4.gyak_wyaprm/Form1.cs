using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace _4.gyak_wyaprm
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> flats;
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        string[] headers = new string[] {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"};

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            flats = context.Flats.ToList();
        }

        object[,] values;

        public void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet; //épp aktív

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
        private void CreateTable()
        {

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            values = new object[flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat flat in flats)
            {
                values[counter, 0] = flat.Code;
                values[counter, 1] = flat.Vendor;
                values[counter, 2] = flat.Side;
                values[counter, 3] = flat.District;
                values[counter, 4] = flat.Elevator;
                values[counter, 5] = flat.NumberOfRooms;
                values[counter, 6] = flat.FloorArea;
                values[counter, 7] = flat.Price;
                //using GetCell for referencies
                string value_1 = GetCell(counter + 2, 6);
                string value_2 = GetCell(counter + 2, 7);
                string value_merged = "=" + value_2 + "/" + value_1;
                values[counter, 8] = value_merged;
                counter++;
            }

            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            FormatTable();
            FormatTable_own();
        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void FormatTable()
        {
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }

        private void FormatTable_own()
        {
            //border
            Excel.Range border = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1)));
            border.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            //first column
            Excel.Range firstColumn = xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), 1));
            firstColumn.Font.Bold = true;
            firstColumn.Interior.Color = Color.LightYellow;

            //last column
            Excel.Range lastColumn = xlSheet.get_Range(GetCell(2, 9), GetCell(1 + values.GetLength(0), 9));
            lastColumn.Interior.Color = Color.LightGreen;

        }

    }
}
}
