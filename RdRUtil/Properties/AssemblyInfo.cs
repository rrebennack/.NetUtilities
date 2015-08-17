using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RdRUtil")]
[assembly: AssemblyDescription("RdR Utilities")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("RdR Solutions")]
[assembly: AssemblyProduct("RdR Utilities")]
[assembly: AssemblyCopyright("RdR Solutions © MMXII - Ron Rebennack - Indianapolis, IN")]
[assembly: AssemblyTrademark("RdR Solutions © MMXII - Ron Rebennack - Indianapolis, IN")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("61829743-4496-4368-a298-93ffe79a5087")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.8.0.0")]
[assembly: AssemblyFileVersion("1.8.0.0")]

/*
 * 06/25/2012 - 1.0.0.0     - Started this cat
 * 10/08/2013 - 1.1.0.0     - Add: Added 'Graphics' class.  Added 'Graphics.GrayScale'
 * 11/20/2013 - 1.1.0.1     - Chg: Util.IsEmpty: Check for a blank date ("0000-00-00 00:00:00")
 * 12/04/2013 - 1.2.0.0     - Add: 'Util.MyIP()'
 * 12/06/2013 - 1.2.0.1     - Add: 'Util.MyIP()': Added an overload to specify IPV4 vs. IPV6
 * 04/11/2014 - 1.3.0.0     - Add: 'Util.GetInt()' & 'Util.GetDate()'
 *                          - Fix: 'Util.IsNumber()' & 'Util.GetNumber()': Check each charater rather than TryParse.  Allow '-' & '.'
 * 04/15/2014 - 1.3.0.1     - Fix: 'Graphics.GetColor()': Didn't get the alpha channle.  It now calls 'Graphics.HexToColor()'
 * 07/14/2014 - 1.4.0.0     - Add: 'Util.GetBytes()'
 * 07/24/2014 - 1.4.0.1     - Chg: 'Util.IsEmpty()': Check for a string = "null"
 * 09/15/2014 - 1.5.0.0     - Add: 'Util.GetEnumValues()'
 * 03/25/2015 - 1.6.0.0     - Add: 'Graphics.RotateImage()'
 * 03/26/2015 - 1.6.0.1     - Fix: 'Graphics.RotateImage()': Use high quality image
 * 04/08/2015 - 1.7.0.0     - Add: 'Graphics.ResizeImage()'
 * 04/16/2015 - 1.7.0.1     - Fix: 'Util.GetInt()': If the value passed has a decimal, round the integer, don't just drop the decimal.
 * 08/17/2015 - 1.8.0.0     - Add: 'Graphics.SetImageOpacity()'
*/