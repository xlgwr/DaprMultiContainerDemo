 ## 启动管理中台
 ```
 kubectl proxy
 $TOKEN=((kubectl -n kube-system describe secret default | Select-String "token:") -split " +")[1]
 kubectl config set-credentials docker-for-desktop --token="${TOKEN}"
 echo $TOKEN
 ```
 ## 重启对象
 ```
 kubectl get pod {podname} -n {namespace} -o yaml | kubectl replace --force -f -
 ```
 ## 最新的 yaml 文件
 ```
  kubectl replace --force -f xxxx.yaml 
 ```
