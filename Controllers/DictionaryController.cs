using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Dictionary_Site.Controllers;

public class DictionaryController : Controller
{
    // 
    // GET: /Dictionary/
    public string Index()
    {
        return "This is my default action...";
    }
    // 
    // GET: /Dictionary/Welcome/ 
    public string Welcome()
    {
        return "This is the Welcome action method...";
    }
}