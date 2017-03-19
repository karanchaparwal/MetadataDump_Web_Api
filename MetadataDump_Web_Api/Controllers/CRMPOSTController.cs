using Microsoft.Crm.Sdk.Samples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;
using MetadataDump_Web_Api.Models;

namespace MetadataDump_Web_Api.Controllers
{
    public class CRMPOSTController : ApiController
    {

        public class IDumpEntityInfo
        {
            public string CRM_SERVER { get; set; }
            public string CRM_OFFICE { get; set; }
            public string CRM_UN { get; set; }
            public string CRM_PASS { get; set; }          
            public string CRM_USERNAME { get; set; }

        }
        [HttpPost]
        [Route("api/dump")]
        public object Post([FromBody]IDumpEntityInfo model)
        {
            List<string> ListAllEntities = new List<string>();
            List<string> CustomEntities = new List<string>();

            //  List<string> a = new List<string>();
            DumpEntityInfo x = new DumpEntityInfo();
            try
            {
                 x.Main(model.CRM_SERVER,model.CRM_OFFICE,model.CRM_UN,model.CRM_PASS);
                //x.MainM(model.CRM_SERVER, model.CRM_OFFICE, model.CRM_UN, model.CRM_PASS);
                XmlTextReader reader = new XmlTextReader("E:/celebal/MetadataFinal/metadata/MetadataDump_Web_Api/MetadataDump_Web_Api/bin/EntityInfo.xml");
                XmlNodeType type;
                while (reader.Read())
                {
                    type = reader.NodeType;
                    if (type == XmlNodeType.Element)
                    {
                        if (reader.Name == "EntitySchemaName")
                        {
                            reader.Read();
                            ListAllEntities.Add(reader.Value);
                        }
                    }
                }
                CustomEntities = ListAllEntities.FindAll(y => y.StartsWith("new_"));

                JsonObject jsonreturnobject = new JsonObject();
                jsonreturnobject.Username = model.CRM_USERNAME;
                jsonreturnobject.TotalEntities = ListAllEntities.Count();
                jsonreturnobject.AllEntitiesNames = ListAllEntities.ToArray();
                jsonreturnobject.TotalCustomEntities = CustomEntities.Count();
                jsonreturnobject.CustomEntitiesNames = CustomEntities.ToArray();
               // var json = JsonConvert.SerializeObject(jsonreturnobject);
              //  Dictionary<string,>
                //      Dictionary<string, string> jsonarray = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                //  var jsonObject = (JObject)JsonConvert.DeSerializeObject(json);
                //  string[][] newKeys = CustomEntities.Select(y => new string[]{y}).ToArray();
                // var json = JsonConvert.SerializeObject(newKeys);
                //  string s = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);

                return jsonreturnobject;

            }
            catch (Exception ex)
            {
                string a = ex.Message + ":" + ex.StackTrace;
                return a;
            }
        }
    }
}