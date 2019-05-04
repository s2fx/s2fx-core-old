#!/usr/bin/env groovy

stage('compile') {
    node {
        // GIT submodule recursive checkout
        checkout scm 
        sh 'dotnet restore'
        dir('client/s2fx-client-typescript') {
            sh 'yarn install -silent'
            sh 'yarn build --silent'
        }
        dir('client/s2fx-coreui-angular') {
            sh 'yarn install -silent'
            sh 'yarn --silent "build-lib"'
        }
        sh 'dotnet build -c Release'
    }
}

stage('test') {

    unitTests: {
        test('DotnetUnitTest')
        test('JsUnitTest')
    } 

    integrationTests: {
        test('IntegrationTest')
    }
}

stage('pack') {
    node {
        sh 'dotnet pack -c Release --no-build'
    }
}

def test(type) {
    if (type == 'DotnetUnitTest') {
        node {
            sh 'dotnet test -c Release ./test/S2fx.Tests/S2fx.Tests.csproj'
        }
    }
    else if (type == 'JsUnitTest') {
        //TODO
    }
    else if (type == 'IntegrationTest') {
        dir('client/s2fx-client-typescript') {
            sh 'yarn test'
        }
    }
}

