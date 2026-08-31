using System.Reflection;


//Asselmbly repräsentiert eine DLL im Arbeitsspeicher

//Assembly.LoadFrom -> laden einer DLL 
Assembly geladeneneDll = Assembly.LoadFrom("Taschenrechner.dll");

Type taschenrechnerClassAsType = geladeneneDll.GetType("Taschenrechner.MyCalc");

//Merke mir den Einstiegspunkt der Instanz MyCalc
object tr = Activator.CreateInstance(taschenrechnerClassAsType);

//Native .NET Datentypen müssen angegenen werden
MethodInfo methodeInfo = taschenrechnerClassAsType.GetMethod("Add", new Type[] { typeof(Int32), typeof(Int32) });
object result = methodeInfo.Invoke(tr, new object[] { 11, 33 });
Console.WriteLine(result);

Console.ReadLine();