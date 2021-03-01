FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN apt-get update

#Install Google Chrome
#Check available versions here: https://www.ubuntuupdates.org/package/google_chrome/stable/main/base/google-chrome-stable
ARG CHROME_VERSION="84.0.4147.89-1"
ARG CHROMEDRIVER_VERSION="84.0.4147.3001"

RUN wget --no-verbose -O /tmp/chrome.deb http://dl.google.com/linux/chrome/deb/pool/main/g/google-chrome-stable/google-chrome-stable_${CHROME_VERSION}_amd64.deb \
  && apt install -y /tmp/chrome.deb \
  && rm /tmp/chrome.deb

#Update Nuget
RUN nuget update -self
RUN dotnet nuget add source http://www.nuget.org/api/v2/ --name nuget_v2

#Copy sourcecode into image and do compilation
RUN mkdir /src
COPY qa/ /src
WORKDIR /src
RUN dotnet restore ${APPLICATION_NAME}.sln

#Setup chromedriver and run smoke tests
WORKDIR /root/.nuget/packages
#RUN cp ./selenium.webdriver.chromedriver/${CHROMEDRIVER_VERSION}/driver/linux64/chromedriver /usr/bin/chromedriver
#RUN chmod +x /usr/bin/chromedriver
WORKDIR /src
CMD dotnet test ${APPLICATION_NAME}.sln --filter TestCategory=${TESTCATEGORY} 2>&1 > /src/${APPLICATION_NAME}_testexecution.log || echo "There were failing tests!"
