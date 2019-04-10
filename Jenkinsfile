#!/usr/bin/env groovy

stage('compile') {
    node {
        checkout scm
        stash 'everything'
        sh 'dotnet restore'
        sh 'dotnet build'
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
        unstash 'everything'
        sh 'dotnet test test/S2fx.Tests/S2fx.Tests.csproj'
    }
}

