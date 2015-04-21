using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_Auto_Menus_ByName : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_Menus_ByName()
	{
		this.ViewText = @"from doc in docs.Menus
select new {
	Name = doc.Name
}";
		this.ForEntityNames.Add("Menus");
		this.AddMapDefinition(docs => 
			from doc in ((IEnumerable<dynamic>)docs)
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Menus", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				Name = doc.Name,
				__document_id = doc.__document_id
			});
		this.AddField("Name");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("__document_id");
	}
}
