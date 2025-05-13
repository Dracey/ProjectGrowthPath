# ---- STAGE 1: Build ----
    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
    WORKDIR /app

    # Copy the solution file and restore dependencies for all projects
    COPY ProjectGrowthPath.sln ./
    COPY ProjectGrowthPath.UserInterface/*.csproj ./ProjectGrowthPath.UserInterface/
    COPY ProjectGrowthPath.Application/*.csproj ./ProjectGrowthPath.Application/
    COPY ProjectGrowthPath.Infrastructure/*.csproj ./ProjectGrowthPath.Infrastructure/
    COPY ProjectGrowthPath.Domain/*.csproj ./ProjectGrowthPath.Domain/
    
    # Restore all projects
    RUN dotnet restore
    
    # Copy all files and build the app
    COPY . ./
    RUN dotnet publish ProjectGrowthPath.UserInterface/ProjectGrowthPath.UserInterface.csproj -c Release -o /out
    
    # ---- STAGE 2: Runtime (for serving static files) ----
    FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
    WORKDIR /app
    
    # Copy the published output from the build stage
    COPY --from=build /out .
    
    # Expose port 80
    EXPOSE 80
    ENV ASPNETCORE_URLS=http://+:80
	
 	   

    # Set the entry point to serve the Blazor WASM application
    ENTRYPOINT ["dotnet", "ProjectGrowthPath.UserInterface.dll"]