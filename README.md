# StoryBot

## 介绍
基于[彩云小梦尝鲜版](http://if.caiyunai.com/dream)的续写Bot

## 配置字段介绍
### 基础文本
```ini
[Config]
UID=
```
**有*为必填字段**
修改及时生效, 除了UID
- MID: 模型ID [默认拉取模型列表第一个]
- UID*: 用户ID
- Font: 字体设置, 必须为系统内安装的字体 [默认`微软雅黑`]
- PicWidth: 最终生成图片的宽度(像素), 至少20 [900]
- ThinkText: 响应文本, 使用`|`分割 [emmmm|让我想想...|我试试能写出点啥...|难内...]

## 获取MID以及UID
1. 使用谷歌系浏览器打开[彩云小梦尝鲜版官网](http://if.caiyunai.com/dream), 进行登录或注册
2. 打开开发者工具, 跳转至网络页
3. 刷新页面
4. 过滤器内填`info`, 单击左侧的结果, 复制URL中`http://if.caiyunai.com/v2/user/6191f39xxxxxxxx6d20xxx2/info`的`user与info`中间的`6191f39xxxxxxxx6d20xxx2`, 这个是UID
5. 过滤器内填`model_list`, 单击左侧的结果, 右侧顶部选项卡选择第二个`预览`, 展开Json, 即可看到`mid`, 按照自己的喜好填写即可