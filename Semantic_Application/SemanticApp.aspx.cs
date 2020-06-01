using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Parsing.Contexts;
using VDS.RDF.Parsing.Events;
using VDS.RDF.Parsing.Events.RdfXml;
using VDS.RDF.Parsing.Handlers;
using VDS.RDF.Parsing;
using System.Xml;
using System.Data;
using VDS.RDF.Writing.Formatting;
using System.Text;

namespace Semantic_Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string filename = @"\RestaurantScenerio.rdf-xml.owl";
            //get the owl file path
            string owlfilepath = AppDomain.CurrentDomain.BaseDirectory + filename;

            //var content = File.ReadAllText(owlfilepath);
            //var graph = new VDS.RDF.Graph();
            //var xml = new XmlDocument();
            //xml.LoadXml(content);
            //var parser = new RdfXmlParser();
            //parser.Load(graph, xml);

            IGraph graph = new Graph();
            graph.LoadFromFile(owlfilepath);

            string query = TextBox1.Text;
            string str = "PREFIX Res: <http://www.cs.le.ac.uk/rdf#>"
                + "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>" + query;
        

            Object res = graph.ExecuteQuery(str);
            SparqlResultSet results = (SparqlResultSet)res;
            DataTable table = new DataTable();
            DataRow row;

            INodeFormatter formatter = new TurtleFormatter();
           
            switch (results.ResultsType)
            {
                case SparqlResultsType.VariableBindings:
                    foreach (String var in results.Variables)
                    {
                        table.Columns.Add(new DataColumn(var, typeof(INode)));
                    }

                    foreach (SparqlResult r in results)
                    {
                        row = table.NewRow();
                        r.ToString(formatter);
                        foreach (String var in results.Variables)
                        {
                            if (r.HasValue(var))
                            {
                                row[var] = r[var];
                            }
                            else
                            {
                                row[var] = null;
                            }
                        }
                        table.Rows.Add(row);
                    }
                    break;
                case SparqlResultsType.Boolean:
                    table.Columns.Add(new DataColumn("ASK", typeof(bool)));
                    row = table.NewRow();
                    row["ASK"] = results.Result;
                    table.Rows.Add(row);
                    break;

                case SparqlResultsType.Unknown:
                default:
                    throw new InvalidCastException("Unable to cast a SparqlResultSet to a DataTable as the ResultSet has yet to be filled with data and so has no SparqlResultsType which determines how it is cast to a DataTable");
            }
            populatedata(table);
        }
         public void populatedata(DataTable dt)
        {
            StringBuilder html = new StringBuilder();
            //Table start.
            html.Append("<table border = '1'>");
            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }
            //Table end.
            html.Append("</table>");
            string strText = html.ToString();
            ////Append the HTML string to Placeholder.
            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
        }
    }
}