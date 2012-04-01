Scrumee
-------

###About###

Scrumee is a very simple SCRUM-inspired project management solution. This project is by no means meant to be used as a real solution but meant to demonstrate .NET software components and frameworks working together for educational purposes only.

###Technologies###

Scrumee uses ASP.NET MVC 3 with a SQLite database backend bridged together using NHibernate.

Frameworks and libraries used:
  
  - ASP.NET MVC 3 with Razor Views
  - NHibernate v3.2 ( ORM )
  - StructureMap v2.6.1 ( Dependency Injection)
  - System.Data.SQLite v1.0.66.0 ( ADO.NET adapter for SQLite )

###Implementation###

This implementation of Scrumee uses the repository pattern and dependency injection for the separation of concerns benefits.

The NHibernate session management is handled via StructureMap where the SessionFactory is registered as a Singleton for the life of the application and any requests for a new ISession are stored within the current HttpContext thereby making the NHibernate session reusable multiple times if need be during a single request.

The repository also makes use of a technique I am coining as **"The Lazy-Loaded Session-Per-Request Pattern"**. The very simple difference being that the creation of the ISession is "lazy" in that  it is never created needlessly with each request but only created when needed. The standard practice of injecting an ISession into the constructor of a repository can easily lead to situations where NHibernate sessions are being created needlessly such as requests for static pages that never utilize database-driven content such as a typical About web page.

This implementation of NHibernate also utilizes the standard .HBM XML mapping files and the Loquacious ( NHibernate v3.0+ ) configuration API.

###Note###

This project is one in a series of projects utilizing Scrumee as the base application to demonstrate different software libraries and frameworks. All of the projects can be found here on Github.com: [https://github.com/randyburden](https://github.com/randyburden)
