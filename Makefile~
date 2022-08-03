# Project Variables
PROJECT_NAME ?= AdminPanel
ORG_NAME ?= AdminPanel
REPO_NAME ?= AdminPanel

.PHONY: migrations db

migrations:
	cd ./Adminpanel.Data && dotnet ef --startup-project ../AdminPanel.Web/ migrations add $(mname) && cd ..

db:
	cd ./Adminpanel.Data && dotnet ef --startup-project ../AdminPanel.Web/ database update && cd ..
