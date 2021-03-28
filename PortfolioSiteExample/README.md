# Introduction

TODO

## Run the Console App

Run the following commands on your local machine in the terminal:
```bash
git clone https://github.com/portfolio-site-demo/example-project.git
cd example-project/PortfolioSiteExample.ConsoleApp
dotnet run
```

**Optional:** Run the Console App using Katacoda
1. Navigate to: https://www.katacoda.com/courses/container-runtimes/what-is-a-container
2. Select `START SCENARIO`.
3. Install .NET Core 3.1 by running the following commands in the terminal:
	```bash
	sudo apt-get install -y gpg
	wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
	sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
	wget -q https://packages.microsoft.com/config/ubuntu/19.04/prod.list
	sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
	sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
	sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
	sudo apt-get install -y apt-transport-https
	sudo apt-get update
	sudo apt-get install -y dotnet-sdk-3.1
	```
4. Run the Console App:
	```bash
	git clone https://github.com/portfolio-site-demo/example-project.git
	cd example-project/PortfolioSiteExample.ConsoleApp
	dotnet run
	```

## Console App - Example Output

TODO

## Run the Docker Compose App

**Steps**
1. Navigate to: https://www.katacoda.com/courses/container-runtimes/what-is-a-container
2. Select `START SCENARIO`.
3. Run the following commands in the terminal:
	```bash
	git clone https://github.com/portfolio-site-demo/example-project.git
	cd example-project/PortfolioSiteExample.DockerComposeApp
	docker-compose up
	```
4. Wait several minutes for Docker Compose to generate multiple application containers:
   - MySQL Database
   - phpMyAdmin
   - PortfolioSiteExample.Api
   - PortfolioSiteExample.Frontend
5. Once the containers are running, select the `+` (plus) sign at the top of the terminal and then select `View HTTP port 80 on Host 1`.

## Docker Compose App - Example Output

TODO