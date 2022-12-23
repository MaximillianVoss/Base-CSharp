using Custom_Controls_WF.Tests.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Custom_Controls_WF.Tests.Tests
{
    [TestClass]
    public class PlotTests
    {
        [TestMethod]
        public void TestPreview()
        {
            PlotWindow plotWindow = new PlotWindow();
            plotWindow.ShowDialog();
        }
    }
}
