# AspNet_WebApi2_Security_MessageHandlers

A look at message handlers in an ASP.Net - Wep Api 2 app.

---

Developed with Visual Studio 2015 Community

---

###Techs
|Tech|
|----|
|C#|
|EntityFramework|
|ASP.Net Identity|
|Web Api 2|
|OWIN|

---

###Brief Description

We have a Web Api app with 2 message handlers; 1 for checking user credentials and the other for checking an api key which is set as a custom header. 

Both handlers must be successful for the request to proceed down the pipeline.

If the users credentials are authenticated, we set the identity principal within the requests context. This allows the user to access an [Authorized] end point.

#### TESTING

Run the Api first, then the console app.

The console app uses HttpClient to make 4 calls to the web service of which only one will succeed.

---

###Resources
|Title|Author|Website|
|-----|------|-------|
|[HTTP Message Handlers in ASP.NET Web API](https://www.asp.net/web-api/overview/advanced/http-message-handlers)|Mike Wasson|MSDN|
|[Web API 2 security extensibility points part 3: custom message handlers](https://dotnetcodr.com/2015/07/27/web-api-2-security-extensibility-points-part-3-custom-message-handlers/)|Andras Nemes|dotnetcodr|
