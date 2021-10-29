#!/bin/bash

arr=($(find ./ -name '*.csproj'))
for i in "${arr[@]}"; do
    echo "restoring $i"
    dotnet restore ./$i
done

