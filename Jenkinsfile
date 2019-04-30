#!/usr/bin/env groovy

stage('compile') {
    node {
        checkout scm
        sh 'git submodule update --init --recursive'
        sh 'dotnet restore'
        dir('client/s2fx-client-typescript') {
            sh 'yarn install -silent'
            sh 'yarn build --silent'
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

