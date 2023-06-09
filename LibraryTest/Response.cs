using A3.Backend.Persistence.Context;
using A3.Backend.Persistence.Repository;

namespace LibraryTest;
public class Response
{
    public Response()
    {

    }

    public string GetResponse(string response)
    {


        return $"This is your response: {response}";
    }

    public string GetAll(string connection)
    {
        try
        {
            using (var db = new GenieContext(connection))
            {
                var unitOfWork = new UnitOfWorkService(db);

                var response = unitOfWork.Customers.Get(1);

                return response.CustomerName;

            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
}

