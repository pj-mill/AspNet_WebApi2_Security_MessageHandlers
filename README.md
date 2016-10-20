# AspNet_WebApi2_Security_MessageHandlers

A look at message handlers in an ASP.Net - Wep Api 2 app.

---

Developed with Visual Studio 2015 Community

---

###Techs
|Tech|
|----|
|C#|

---

###Brief Description

We have a Web Api app with 2 message handlers; 1 for checking user credentials and the other for checking an api key which is set in a custom header.

The console app uses HttpClient to make 4 calls to the web service of which only one will succeed.

---

###Resources
|Title|Author|Website|
|-----|------|-------|
|[HTTP Message Handlers in ASP.NET Web API](https://www.asp.net/web-api/overview/advanced/http-message-handlers)|Mike Wasson|MSDN|
|[Web API 2 security extensibility points part 3: custom message handlers](https://dotnetcodr.com/2015/07/27/web-api-2-security-extensibility-points-part-3-custom-message-handlers/)|Andras Nemes|dotnetcodr|