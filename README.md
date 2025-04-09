<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="[https://github.com/github_username/repo_name](https://github.com/Dracey/ProjectGrowthPath)">
    <img src="![image](https://github.com/user-attachments/assets/fb4c28ef-c97d-46c1-87cb-6abc71261079)" alt="Logo" width="90" height="80">
  </a>

<h3 align="center">Project GrowthPath</h3>

 ## Over het project
 <p align="center">
    Project GrowthPath is een leerplatform ontwikkeld om medewerkers binnen het innovatielab te helpen bij hun professionele groei met behulp van gamificatie-elementen. Het helpt medewekers met het vinden van leermateriaal die passen bij hun interesses en bestaande vaardigheden om  hun professionele zelf verder te ontwikkelen. 
    <br />
  </p>
</div>

## Features
* Dashboard voor voortgang
* Gamificatie van het proces
* Gepersonaliseerde leeradviezen gebaseerd op doelen

### Technologieën:
Deze volgende stack en tools zijn gebruikt in dit project:
* Fron-end: Blazor Server
* Back-end: ASP.NET Core (.NET 9)
* Database: PostgreSQL 17.0

<!-- GETTING STARTED -->
## Lokaal draaien 
Om dit project lokaal te draaien, volg de onderstaande stappen:

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Benodigheden
Wat is er allemaal nodig om het project lokaal te kunnen draaien:
* Visual Studio
* .NET 9 SKD
* PostgreSQL

### Installatie
1. Kopieer het project:
   ```sh
   git clone https://github.com/gebruikersnaam/ProjectGrowthPath.git
   ```
2. Installeer de benodigde tools (zie benodigheden)
3. Maak een nieuwe database aan `GrowthPathDB` in PostgreSQL
4. Update de connectiestring in `appsettings.json`:
   ```js
   "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=GrowthPathDB;Username=postgres;Password=yourpassword"
    }
   ```
5. Voer een database migratie uit:
   ```sh
   dotnet ef database update
   ```
6. Voer de applicatie uit:
   ```sh
   dotnet run --project ProjectGrowthPath.UI
   ```
## Architectuur
De applicatie is opgezet met behulp van Clean Architecture. 
Bestaat uit de volgende lagen: 
* Domain: bevat de kernlogica van de applicatie, inclusief de eniteiten.
* Application: bevat de use cases van het systeem
* Infrastructure: bevat de externe services en database connectie
* UI: Blazor Server componenten voor gebruikersinterface. 

