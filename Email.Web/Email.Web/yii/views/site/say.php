<?php
use yii\helpers\Html;
?>
<?= Html::tag('div',Html::encode('信息'),['class'=>"panel"]) ?>
<?php 
$options=['style'=>'width:100px;height:100px;border:1px solid red'];
if($message){
    Html::addCssStyle($options,"color:blue");
}
?>
<?= Html::beginForm(['country/index']) ?>
<input type="text" name="name" value="llm">
<?= Html::endForm()?>

<?= Html::tag('b','粗体',$options ) ?>
