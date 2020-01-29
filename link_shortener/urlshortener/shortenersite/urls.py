from django.urls import re_path, path, include
from shortenersite import views

urlpatterns = [
    path('', views.index, name = 'home'),
    re_path(r'^(?P<short_id>\w{6})$', views.redirect_original, name = 'redirectoriginal'),
    path('makeshort/', views.shorten_url, name = 'shortenurl'),
]
