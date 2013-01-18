using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Walker.Tests
{
    [TestClass]
    public class BuilderWalkerIntegrationTests
    {
        private static Stream TransitionDocument
        {
            get { return Assembly.GetExecutingAssembly().GetManifestResourceStream("Walker.Tests.TransitionDocument.xml"); }
        }

        [TestMethod]
        public void Transition_Document_Loads()
        {
            var document = XDocument.Load(TransitionDocument);
            Assert.IsNotNull(document);
            Assert.IsTrue(document.Descendants().Any());
        }

        [TestMethod]
        public void Transition_Document_Generates_Graph()
        {
            var document = XDocument.Load(TransitionDocument);
            var builder = new WorkItemStateGraphBuilder();
            var graph = builder.BuildStateGraph(document);

            Assert.IsNotNull(graph);            
        }

        [TestMethod]
        public void Transition_Document_Graph_Is_Walkable()
        {
            var document = XDocument.Load(TransitionDocument);
            var builder = new WorkItemStateGraphBuilder();
            var graph = builder.BuildStateGraph(document);
            var walker = new GraphWalker<string>(graph);

            var walkingPath = walker.TraverseTo("Closed");

            Assert.IsNotNull(walkingPath);
        }

        [TestMethod]
        public void Transition_Document_Graph_Is_Searchable()
        {
            var document = XDocument.Load(TransitionDocument);
            var builder = new WorkItemStateGraphBuilder();
            var graph = builder.BuildStateGraph(document);

            var node = graph.Find("Failed Testing");
            
            Assert.IsNotNull(node);
        }

        [TestMethod]
        public void Search_Result_Is_Walkable()
        {
            var document = XDocument.Load(TransitionDocument);
            var builder = new WorkItemStateGraphBuilder();
            var graph = builder.BuildStateGraph(document);
            var node = graph.Find("Failed Testing");

            var walker = new GraphWalker<string>(node);
            var walk = walker.TraverseTo("Closed");

            Assert.IsNotNull(walk);
        }
    }
}
