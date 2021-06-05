using ImporterModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ImporterXML
{
	[XmlType(TypeName = "Contents")]
	public class Contents : List<Content> { }
}
