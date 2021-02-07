Before runnig solution from ..\Shop.Database in console run   dotnet ef -s ../Shop.UI migrations add init
                                                       then   dotnet ef -s ../Shop.UI database update
                                                       
Now you can build project.
On build there are created two accounts 'Admin' password 'password' :) and 'Customer' password 'password'
Each account has own features so try both.
