# chegg-solutions-saver
Because Chegg only gave few days to access their solutions for free when I rent their books so I decided to create a web crawler to save all the needed solutions.
# Technologies
I created this prototype after few hours so I just used simple and easy to implement, but the idea can be replicate in any languages.
- C# winform (You need to have Visual Studio)
  - I used <a href="https://www.nuget.org/packages/HtmlAgilityPack/" target="_blank">HtmlAgilityPack</a> for parsing Html.
  - I used awesome <a href="https://github.com/cefsharp/CefSharp" target="_blank">CefSharp</a> for interacting with web pages.

# How to use
First, you need to have a Chegg account that has access to the book solutions you are going to crawl.

After that, go to Chegg website and grab **the login redirect url of the first solution page**. For example: `https://www.chegg.com/auth?action=login&redirect=https%3A%2F%2Fwww.chegg.com%2Fhomework-help%2Fsurvey-of-accounting-8th-edition-chapter-a-problem-1e-solution-9781337517386&reset_password=0`

Run the application, provide the url into the first box, username, password and finally the result folder.
