using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //dynamic obj = new WroxDynamicObject();
            //obj.fn = "bibekananda";
            //obj.ln = "Panigrahi";
            //obj.office = "Black Knight Financial Service";
            //Func<DateTime, string> GetTomorrow = today => today.AddDays(1).ToShortDateString();
            //obj.todate = GetTomorrow;
            //Console.WriteLine(obj.todate(DateTime.Now));
            //Console.WriteLine(obj.GetType());
            //Console.WriteLine(obj.fn+" "+obj.ln+"  "+ obj.office);
            //  Console.WriteLine("Hello World!");
            ExpandoObject();
        }
        static void ExpandoObject()
        {
            dynamic obj = new ExpandoObject();
            obj.FirstName = "Mr. Bibekananda";
            obj.LastName = "Panigrahi";
            Console.WriteLine($"first Name {obj.FirstName} and last Name {obj.LastName}");
            Func<DateTime, string> getTomorrow = today => today.AddDays(2).ToShortDateString();
            obj.getTomorrowDate = getTomorrow;
            Console.WriteLine(obj.getTomorrowDate(DateTime.Now));
            Console.Read();
        }


    }
    public class WroxDynamicObject:DynamicObject
    {
        private Dictionary<string, object> _dynamicdata = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool sucess = false;
            result = null;
            if(_dynamicdata.ContainsKey(binder.Name))
            {
                result = _dynamicdata[binder.Name];
                sucess = true;
            }
            else
            {
                result ="Result Not Found.....";
            }
            return sucess;  
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dynamicdata[binder.Name] = value;
            return true;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            dynamic method = _dynamicdata[binder.Name];
            result = method((DateTime)args[0]);
            return result != null;
        }
    }
}
