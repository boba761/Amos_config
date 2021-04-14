using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Amos.Data;
using Calculations.Variables;

namespace Amos.TypeFiles.FileBuilders
{
    public class ConfigDashboardFileBuilder : FileBuilder
    {
        public override void Load(Stream stream)
        {
             XDocument document = XDocument.Load( stream );
             Document.Dashboard.CurrentCollectionVariable.Clear();
             GetVariable( Document.Dashboard.CurrentCollectionVariable, document.Root );
        }

        public override void Save(Stream stream)
        {
            XDocument document = new XDocument(new XElement("ConfigDashboard"));
            SetElenent( document.Root, Document.Dashboard.CurrentCollectionVariable.VariableChilds );
            document.Save( stream );
        }

        private void SetElenent( XElement element, ICollection<VariableBase> variables )
        {
            foreach ( VariableBase variable in variables )
            {
                if ( variable is Variable )
                    element.Add( new XElement( "Variable", variable.Name ) );
                else
                {
                    XElement group = new XElement("GroupVariable", new XAttribute("name", variable.Name));
                    element.Add( group );
                    SetElenent( group, (variable as CollectionVariable).VariableChilds );
                }
            }
        }

        private void GetVariable( CollectionVariable variable, XElement element )
        {
            foreach ( XElement xElement in element.Elements() )
            {
                if ( xElement.Name == "Variable" )
                    variable.Add( Document.Dashboard[xElement.Value] );
                else if ( xElement.Name == "GroupVariable" )
                {
                    CollectionVariable collection = new CollectionVariable( xElement.Attribute( "name" ).Value );
                    variable.Add( collection );
                    GetVariable( collection, xElement );
                }
            }
        }
    }
}
