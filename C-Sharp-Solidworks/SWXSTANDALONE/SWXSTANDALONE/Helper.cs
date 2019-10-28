using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SolidWorks.Interop.sldworks;
using System.Threading;
using SolidWorks.Interop.swconst;

namespace SWXSTANDALONE
{
    internal static class Helper
    {

        internal static bool processModel(SldWorks swApp, string file, List<CustomPropertyObject> CustomProperties, CancellationToken cancellationToken)
        {
            int Warning = 0;
            int Error = 0;

            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                string extension = Path.GetExtension(file);
                int type = 0;
                if (extension.ToLower().Contains("sldprt"))
                {
                    type = (int)swDocumentTypes_e.swDocPART;
                }
                else
                {
                    type = (int)swDocumentTypes_e.swDocASSEMBLY;
                }

                ModelDoc2 swModel = swApp.OpenDoc6(file, type, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref Error, ref Warning) as ModelDoc2;

                if(Error != 0)
                    return false;

                if (swModel == null)
                    return false;

                swModel.Visible = false;
                foreach(CustomPropertyObject obj in CustomProperties)
                {
                    CustomPropertyManager customPropertyManager = swModel.Extension.CustomPropertyManager[""];
                    if (obj.Delete)
                    {
                        DeleteCustomProperty(customPropertyManager, obj.Name);
                    }
                    else
                    {
                        ReplaceCustomPropertyValue(customPropertyManager, obj.Name, obj.Value, obj.NewVal);
                    }
                    swModel.SaveSilent();
                    swApp.QuitDoc(swModel.GetTitle());

                    swModel = null;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static string[] getCADFilesFromDirectory(string directory)
        {
            if((System.IO.Directory.Exists(directory))){
                return null;
            }




            //get sldparts from dir
            string[] parts = Directory.GetFiles(directory, "*.sldprt");
            string[] assemblies = Directory.GetFiles(directory, "*.sldasm");

            List<string> files = new List<string>();


            if (parts != null) 
            files.AddRange(parts);
            if(assemblies != null)
            files.AddRange(assemblies);

            return files.ToArray();
        } 
        internal static bool ReplaceCustomPropertyValue(CustomPropertyManager customPropertyManager, string CustomProperty, string Replaceable, string Replacing) {

            string val = customPropertyManager.Get(CustomProperty);
            if (val == null)
            {
                if (val.ToLower().Contains(Replaceable.ToLower()))
                {
                    string NewVal = val.Replace(Replaceable, Replacing);
                    int ret = customPropertyManager.Set(CustomProperty, NewVal);

                    if (ret == 0)
                        return true;
                    else
                        return false;
                }

                

            }
            return false;
        }
        internal static bool DeleteCustomProperty(CustomPropertyManager customPropertyManager, string CustomProperty)
        {
            //get custom property value
            var Val = customPropertyManager.Get(CustomProperty);
            
            //check if custom Property exists
            if(!string.IsNullOrEmpty(CustomProperty))
            {
               int ret = customPropertyManager.Delete(CustomProperty);
                if (ret == 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
