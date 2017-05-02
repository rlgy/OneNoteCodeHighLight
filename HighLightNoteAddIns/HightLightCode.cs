using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Extensibility;
using Microsoft.Office.Interop.OneNote;
using Microsoft.Office.Core;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Reflection;
using HighLightBuild;


namespace HighLightNoteAddIns
{



    //{B2727A93-9C8E-412B-B6E6-4C836B358AFF}
    [ComVisible(true)]
    [Guid("D5ECCD00-CF2D-409B-B65A-BDBACB9F21DB"), ProgId("HighLightNote")]
    public class HighLightCode : IDTExtensibility2, IRibbonExtensibility
    {

        private XNamespace _ns;

        Microsoft.Office.Interop.OneNote.Application onApp = new Microsoft.Office.Interop.OneNote.Application();
        //private object application;
        public string GetCustomUI(string RibbonID)
        {
            return Properties.Resources.ribbon;
        }

        public void OnAddInsUpdate(ref Array custom)
        {

        }

        public void OnBeginShutdown(ref Array custom)
        {
            //if (application != null)
            //    application = null;
            if (onApp != null)
                onApp = null;
        }

        public void OnConnection(object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
        {
            //application = Application;
            onApp = (Microsoft.Office.Interop.OneNote.Application)Application;
        }

        public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
        {
            //application = null;
            onApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void OnStartupComplete(ref Array custom)
        {

        }

        public void onStart(IRibbonControl control)
        {
            string fileName = Guid.NewGuid().ToString();
            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.WorkingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));


                //MessageBox.Show(info.WorkingDirectory);

                info.FileName = "HighLightForm.exe";
                info.Arguments = " " + fileName;
                info.WindowStyle = ProcessWindowStyle.Normal;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();
                p.WaitForInputIdle();
                if (!p.HasExited)
                    p.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行HighLightForm.exe出错：" + ex.Message);
                return;
            }
            string outFileName = Path.Combine(Path.GetTempPath(), fileName + ".html");

            if (File.Exists(outFileName))
                insertCodeToCurrentSide(outFileName);

        }
        public IStream GetImage(string imageName)
        {
            MemoryStream mem = new MemoryStream();
            Properties.Resources.Logo.Save(mem, ImageFormat.Png);
            return new CCOMStreamWrapper(mem);
        }

        private void insertCodeToCurrentSide(string fileName)
        {
            //string htmlContent = File.ReadAllText(fileName, Encoding.UTF8);



            string noteBookXml;
            onApp.GetHierarchy(null, HierarchyScope.hsPages, out noteBookXml);

            var doc = XDocument.Parse(noteBookXml);
            _ns = doc.Root.Name.Namespace;

            var pageNode = doc.Descendants(_ns + "Page")
                .Where(n => n.Attribute("isCurrentlyViewed") != null && n.Attribute("isCurrentlyViewed").Value == "true")
                .FirstOrDefault();

            string SelectedPageID = pageNode.Attribute("ID").Value;

            string SelectedPageContent;
            onApp.GetPageContent(SelectedPageID, out SelectedPageContent, PageInfo.piSelection);
            var SelectedPageXml = XDocument.Parse(SelectedPageContent);

            pageNode = SelectedPageXml.Descendants(_ns + "Page").FirstOrDefault();
            //pageNode.
            XElement pointNow = pageNode
                .Descendants(_ns + "T").Where(n => n.Attribute("selected") != null && n.Attribute("selected").Value == "all")
                .First();
            if (pointNow != null)
            {

                var isTitle = pointNow.Ancestors(_ns + "Title").FirstOrDefault();

                if (isTitle != null)
                {
                    MessageBox.Show("代码不能插入标题中");
                    return;
                }

            }
            else
            {
                return;
            }
            //MessageBox.Show(pageNode.ToString());
            //return;
            try
            {
                XmlBuild builder = new XmlBuild(fileName, _ns);


                builder.XmlReBuilding(ref pageNode, ref pointNow);

                //MessageBox.Show(pageNode.ToString());
                onApp.UpdatePageContent(pageNode.ToString(), DateTime.MinValue);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
