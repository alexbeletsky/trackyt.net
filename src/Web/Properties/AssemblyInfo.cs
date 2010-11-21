using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Enables pure unit testing of JSon (used in API)
// details: http://www.heartysoft.com/post/2010/05/25/ASPNET-MVC-Unit-Testing-JsonResult-Returning-Anonymous-Types.aspx
[assembly: InternalsVisibleTo("Trackyt.Core.Tests")] 

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ffae2988-e3c9-4d53-994c-b93d560dcea5")]
