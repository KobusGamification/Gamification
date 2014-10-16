Function CheckRepository($repositoryName){
    svnadmin verify $repositoryName

    If($LASTEXITCODE -eq 0){
        Return $true
    }

    Return $false
}

Function CreateRepos($repositoryName){
    svnadmin create $repositoryName
}

Function DeleteRepos($reposName){
    Remove-Item -Path $reposName -Recurse -Force
}

Function CreateFiles($project){
    md $project
}

Function CheckoutRepos($repos){
    cd "temp"
    svn co $repos
    cd ".."
}

Function GetCurrentedFormatedPath($repos){
    $path = Join-Path -Path $PWD.Path -ChildPath $repos    
    $path = $path.Replace("\", "/")
    $path = "file:///$path"
    write-host $path
    return $path
}

Function CreateRandomFiles($repos1, $f){
    foreach ($i in (1..10)){
        $fileName = "temp\$repos1\test_$i.$f"
        "content" >> $fileName
    }
}

Function AddFiles($repos1){
    cd "temp\$repos1"
    svn add *.*
    cd "..\.."
}

Function CommitFiles($repos1){
    cd "temp"
    svn commit $repos1 -m "Auto commit"
    cd ..
}

Function Main{
    $ErrorActionPreference = "Stop"
    svnadmin help    
    If($LASTEXITCODE -ne 0) {
        Write-Error 'SVN must be in the path variable'
        exit 1
    }
    $repos = @("RepositorioNET", "RepositorioNET2")
    
    foreach($repo in $repos){
        if(Test-Path -Path $repo){
                DeleteRepos $repo
        }
    }
    If(Test-Path -Path "Temp"){
        DeleteRepos "temp"
    }
    md "Temp"
    foreach($repo in $repos){
        
        CreateRepos $repo
        CheckoutRepos (GetCurrentedFormatedPath $repo)
        CreateRandomFiles $repo 'cs'
		CreateRandomFiles $repo 'java'
        AddFiles $repo
        CommitFiles $repo
    }
}

Main
