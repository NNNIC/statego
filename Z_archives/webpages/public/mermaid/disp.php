<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<html lang="ja">
<head>
<meta http-equiv="Content-Type" 
        content="text/html; charset=utf-8">
<title>TEST</title>
</head>

<body>
<p><?php 

    require('dispControl.php');

    $sm = new dispControl();
    $sm->Start();

    while($sm->IsEnd()==FALSE) {
        $sm->Update();        
    }

?> </p>

<script>
  mermaid.initialize({startOnLoad: true, theme: 'neutral'});
</script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/mermaid/8.8.0/mermaid.min.js" integrity="sha512-ja+hSBi4JDtjSqc4LTBsSwuBT3tdZ3oKYKd07lTVYmCnTCor56AnRql00ssqnTOR9Ss4gOP/ROGB3SfcJnZkeg==" crossorigin="anonymous"></script>
</body>
</html>