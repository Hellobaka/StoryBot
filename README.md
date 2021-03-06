# StoryBot

## 介绍
基于[彩云小梦尝鲜版](http://if.caiyunai.com/dream)的续写Bot
### 指令
- #创建续写 新建一篇续写，一个来源只能同时拥有一篇续写，10分钟无操作自动销毁
- #结束续写 结束续写，可重新开始一次
- 续写 [内容(首次必需)] 只有当前来源存在一篇续写时可以调用。第一次使用请加上一个开头，之后再调用可直接写‘续写’两个字

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
![step 4](https://user-images.githubusercontent.com/50934714/142718850-912b6165-f6e9-45f8-ae6a-463e34aadb4d.png)
5. 过滤器内填`model_list`, 单击左侧的结果, 右侧顶部选项卡选择第二个`预览`, 展开Json, 即可看到`mid`, 按照自己的喜好填写即可
![Step 5](https://user-images.githubusercontent.com/50934714/142718855-805f48a2-e841-435c-9c46-2850dcb046ef.png)
