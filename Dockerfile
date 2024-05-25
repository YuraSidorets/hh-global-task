FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY ./JobService.WebAPI/bin/Release/net8.0/publish /app

WORKDIR /app

EXPOSE 5000
EXPOSE 443

USER 1000
ENTRYPOINT ["dotnet", "JobService.WebAPI.dll"]