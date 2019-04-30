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
    parallel unitTests: {
        test('Test')
    }, 
    integrationTests: {
        test('IntegrationTest')
    },
    failFast: false
}

stage('pack') {
    node {
        sh 'dotnet pack -c Release --no-build'
    }
}

def test(type) {
    node {
        sh 'dotnet test -c Release ./test/S2fx.Tests/S2fx.Tests.csproj'
    }
}

