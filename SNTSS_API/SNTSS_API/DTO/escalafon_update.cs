using Aspose.Cells.Rendering;
using System.Data;
using System.Data.OleDb;
using Aspose.Cells;
using System.IO;
using SpreadsheetLight;
using Microsoft.Data.SqlClient;

namespace SNTSS_API.DTO
{
    public class escalafon_update
    {
        public DataTable LoadFromExcelFile(string filePath,string conn)
        {
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("No. Prog", typeof(int)));
                dt.Columns.Add(new DataColumn("Nombre", typeof(string)));
                dt.Columns.Add(new DataColumn("Matrícula", typeof(int)));
                dt.Columns.Add(new DataColumn("Fecha de Registro", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Grupo", typeof(int)));
                dt.Columns.Add(new DataColumn("Calificación", typeof(float)));
                dt.Columns.Add(new DataColumn("Tipo de Contratación", typeof(int)));
                dt.Columns.Add(new DataColumn("Dias Laborados", typeof(int)));
                dt.Columns.Add(new DataColumn("Estatus", typeof(string)));
                dt.Columns.Add(new DataColumn("Observaciones", typeof(string)));
                dt.Columns.Add(new DataColumn("CATEGORIA", typeof(string)));
                dt.TableName = "temp";

                string dir = Directory.GetCurrentDirectory() + '/';
                var pathCombine = Path.Combine(dir, "Multimedia/Escalafon/", filePath);

                using (SLDocument s1 = new SLDocument(pathCombine))
                {
                   
                    int iRow = 2;
                    while (!string.IsNullOrEmpty(s1.GetCellValueAsString(iRow, 1)))
                    {
                        DataRow row = dt.NewRow();
                        row[0] = s1.GetCellValueAsInt32(iRow, 1);
                        row[1] = s1.GetCellValueAsString(iRow, 2);
                        row[2] = s1.GetCellValueAsInt32(iRow, 3);
                        row[3] = s1.GetCellValueAsDateTime(iRow, 4);
                        row[4] = s1.GetCellValueAsInt32(iRow, 5);
                        row[5] = s1.GetCellValueAsDecimal(iRow, 6);
                        row[6] = s1.GetCellValueAsInt32(iRow, 7);
                        row[7] = s1.GetCellValueAsInt32(iRow, 8);
                        row[8] = s1.GetCellValueAsString(iRow, 9);
                        row[9] = s1.GetCellValueAsString(iRow, 10);
                        row[10] = s1.GetCellValueAsString(iRow, 11);
                        dt.Rows.Add(row);
                        iRow++;
                    }
                    s1.CloseWithoutSaving();
                }

                using (SqlConnection sql = new SqlConnection(conn))
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(sql);

                    bulk.DestinationTableName = "temp";
                    bulk.ColumnMappings.Add("No. Prog", "number_escalafon");
                    bulk.ColumnMappings.Add("Nombre", "user_id_escalafon");
                    bulk.ColumnMappings.Add("Matrícula", "Matrícula_scalafon");
                    bulk.ColumnMappings.Add("Fecha de Registro", "date_escalafon");
                    bulk.ColumnMappings.Add("Grupo", "grup_escalafon");
                    bulk.ColumnMappings.Add("Calificación", "qualifications_escalafon");
                    bulk.ColumnMappings.Add("Tipo de Contratación", "type_hiring_escalafon");
                    bulk.ColumnMappings.Add("Dias Laborados", "day_worked_scalafon");
                    bulk.ColumnMappings.Add("Estatus", "status_escalafon");
                    bulk.ColumnMappings.Add("Observaciones", "observaciones");
                    bulk.ColumnMappings.Add("CATEGORIA", "category_escalafon");

                    try
                    {
                        sql.Open();
                        bulk.WriteToServer(dt);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        return dt;
                    }
                }

            }
        }
    }
}
