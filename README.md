# MVVMZip
Collection of learn &amp; practice about MVVM pattern

# MVVM 패턴 정리
## Model/View/ViewModel
### Model
비즈니스 로직, 데이터 객체, 서비스 클라이언트
### View
UI 요소. 화면을 표현하기 위한 객체
### ViewModel
뷰모델은 프레젠테이션 로직을 작성한다. 그리고 뷰의 동작과 상태를 추상화한다. 
프레젠테이션 로직을 작성함에 있어 모델은 뷰모델과 협력한다. 뷰모델은 뷰의 존재를 알지 못한다. 모델의 입장에서 뷰모델은 구체클래스인 뷰를 감추는 역할을 한다. 반대로 뷰의 입장에서는 모델의 존재를 알 수 없다. 그래서 뷰모델은 추상화 역할을 수행한다고 할 수 있다.
다시 정리하면 뷰는 모델에 대한 지식이 없고 뷰모델과 모델은 뷰에 대한 지식이 없다. 
그렇다면 이런 느슨한 연결은 어떻게 구현이 되며, 어떤 이점이 있을까?
## Databinding
뷰와 뷰모델 사이의 느슨한 연결을 구현하는 핵심요소. 느슨한 연결을 유지하면서 소스의 변경 사항을 대상으로 업데이트 하거나 유효성 검사 오류를 뷰에 전송하는 인프라를 제공한다. 데이터 바인딩이 제공하는 인프라를 통해 개발자의 비용을 절감하고 표준화된 방법을 통해 일관적인 패턴을 제공할 수 있다.
## Data Template/Resource
데이터 템플릿은 뷰모델의 데이터 개체와 같은 멤버
뷰모델의 특정 멤버 또는 ViewModel 자체를 어떻게 뷰로 표현할 것인지 결정한다. 리소스 시스템은 자동으로 템플릿을 찾고 적용하도록 할 수 있다.
## Command
tobe...

## References
- https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern
- https://justhackem.wordpress.com/2017/03/05/mvvm-architectural-pattern/
