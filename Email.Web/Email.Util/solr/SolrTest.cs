using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.solr
{
    public class Article
    {
        public Article()
        {
           
        }
        [SolrUniqueKey("id")]
        public int id { get; set; }
        [SolrField("title")]
        public string title { get; set; }

       
    }

   
    public class SolrTest
    {
        public void Fun()
        {
            Startup.Init<Article>("http://localhost:8983/solr/test");
            ISolrOperations<Article> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Article>>();
            //添加
            Article t = new Article {
                id = 3, title = "mm" ,
            };
            var res = solr.Add(t);
            bool success=res.Status == 0;
            //查询
            var qTB = new SolrQueryByField("id", "1");
            // var results = solr.Query(qTB);

            //删除
            // var result=solr.Delete(qTB);
            Console.WriteLine("end");
            
        }
    }
}
