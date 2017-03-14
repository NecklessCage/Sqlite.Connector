using Rdlc.Generator;
using System.Collections.Generic;
using System.Data;

namespace Wpf_Net_4.Hola
{
    public class MainTable
    {
        public static ReportItem Create(string dataSetName, DataTable dt)
        {
            var pocos = new List<SuperPoco>();
            foreach (DataColumn col in dt.Columns)
            {
                pocos.Add(new SuperPoco(col.ColumnName));
            }
            return new Tablix(
                CreateTablixCorner(),
                CreateTablixColumnHierarchy(pocos),
                CreateTablixRowHierarchy(),
                CreateTablixBody(dataSetName),
                dataSetName);
        }

        private static TablixCorner CreateTablixCorner()
        {
            var textRuns1 = new TextRuns(new TextRun { Value = "Group", FontWeight = FontWeight.Bold });
            textRuns1.Add(new TextRun { Value = "", FontFamily = "Wingdings" });
            var textRuns2 = new TextRuns(new TextRun { Value = "", FontFamily = "Wingdings" });
            textRuns2.Add(new TextRun { Value = "Period", FontWeight = FontWeight.Bold });

            var textbox = new Textbox(new Paragraph(textRuns1)) { TextboxStyle = new TextboxStyle() };
            textbox.AddParagraph(new Paragraph(textRuns2));
            return
                new TablixCorner(
                    new TablixCornerRows(new TablixCornerRow(new TablixCornerCell(new CellContents(textbox)))));
        }

        private static TablixColumnHierarchy CreateTablixColumnHierarchy(List<SuperPoco> pocos)
        {
            var group =
                new Group(
                    new GroupExpressions(new GroupExpression("=ColumnName")
                    ));
            
            var sortExpressions = new SortExpressions(new SortExpression(new Value("=ColumnName")));

            var textRun = new TextRun
            {
                Value = "=ColumnName",
                FontWeight = FontWeight.Bold
            };
            var paragraph = new Paragraph(new TextRuns(textRun)) { TextAlign = TextAlign.Center };
            var textbox = new Textbox(paragraph) { TextboxStyle = new TextboxStyle() };
            var header = new TablixHeader(new Inch(0.4), new CellContents(textbox));

            return new TablixColumnHierarchy(new TablixMembers(new TablixMember(group, sortExpressions, header)));
        }

        private static TablixRowHierarchy CreateTablixRowHierarchy()
        {
            var group =
                new Group(
                    new GroupExpressions(new GroupExpression("=ColumnName")));
            /*
            var sortExpression =
                new SortExpression(new Value("=" + Expression.FieldsValue(ElementProperty.Period)));
            var sortExpressions = new SortExpressions(sortExpression);

            var textRun = new TextRun
            {
                Value = "=" + Expression.FieldsValue(ElementProperty.Period),
                FontWeight = FontWeight.Bold
            };
            var paragraph = new Paragraph(new TextRuns(textRun)) { TextAlign = TextAlign.Center };
            var textbox = new Textbox(paragraph)
            {
                TextboxStyle = new TextboxStyle { VerticalAlign = VerticalAlign.Middle }
            };
            var header = new TablixHeader(new Inch(0.7), new CellContents(textbox));
            
            return new TablixRowHierarchy(new TablixMembers(new TablixMember(group, sortExpressions, header)));
            */
            return null;
        }

        private static TablixBody CreateTablixBody(string dataSetName)
        {
            var rectangle = new Rectangle();
            //rectangle.AddReportItem(ElementCell.Create(dataSetName));
            var tablixColumns = new TablixColumns(new TablixColumn(new Width(new Inch(1.05))));
            var tablixCells = new TablixCells(new TablixCell(new CellContents(rectangle)));
            var tablixRows = new TablixRows(new TablixRow(new Inch(0.85), tablixCells));
            return new TablixBody(tablixColumns, tablixRows);
        }
    }
}
