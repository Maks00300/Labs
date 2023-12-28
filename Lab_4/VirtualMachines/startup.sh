cd /var/baget/app
sudo kill -9 $(sudo lsof -t -i:5000)
dotnet Baget.dll --urls http://*:5000