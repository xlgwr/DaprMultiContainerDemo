 ```
 $TOKEN=((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]
 kubectl config set-credentials docker-for-desktop --token="${TOKEN}"
 echo $TOKEN
 ```