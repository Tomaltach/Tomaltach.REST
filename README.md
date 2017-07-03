# Tomaltach.REST
To allow a connection to a Web API using an abstract class. REST service made to save time for later projects.

## Getting Started
**NuGet** > Install-Package Tomaltach.REST

## Usage
Common uses of methods used for API calls.

### Methods
`string BaseURI { get; set; }` 

- An example of the Base URI being **http://HOST:PORT/**

`string Controller {get; set; }` 

- The Controller is the route of the controller you are trying to connect to.

URI so far where **api** is built in:
> **BaseURI/api/Controller** or **http://xxx.xxx.xxx.xxx:xxxx/api/user/**

`Task<string> GET([NotNull] string uri);` 

- The uri here will be the api call with the parameters following, etc, `getUser?id=1`.

`Task<string> POST([NotNull] string uri, [NotNull] string json);` 

- The uri here will be the api call. The json will be the json you are sending.

`Task<string> POST<T>([NotNull] string uri, [NotNull] T model);` 

- The uri here will be the api call. Pass the model and the function will convert it to json and post it.

### Example on how to use
    public interface IUserService : RestService
    {
        // add abstract methods        
        // add UserService specific methods
    }

    public class UserService : IUserService
    {
        public override string BaseURI { get; set; }    
        public override string Controller { get; set; }

        public UserService()    
        {
            BaseURI = "http://192.168.0.1:80/";
            Controller = "user";
        }
        
        public async Task<UserModel> GetUser(int id)
        {
            var json = await GET(String.Format("getUser?id={0}", id));
            return await Task.Run( () => JsonConvert.DeserializeObject<UserModel>(json));
        }
    }
    
    public class Main
    {
        IRestService _userService = new UserService();
    }
