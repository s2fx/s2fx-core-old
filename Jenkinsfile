#!/usr/bin/env groovy

stage('compile') {
    node {
        checkout scm
        sh 'dotnet restore'
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

def test(type) {
    node {
        sh 'dotnet test test/S2fx.Tests/S2fx.Tests.csproj'
    }
}

