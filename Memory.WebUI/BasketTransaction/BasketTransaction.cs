using Memory.WebUI.BasketTransaction.BasketModels;
using Newtonsoft.Json;

namespace Memory.WebUI.BasketTransaction
{
    public class BasketTransaction:IBasketTransaction
    {
        const string basketName = "basket";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketTransaction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeleteBasket()
        {
           _httpContextAccessor.HttpContext.Response.Cookies.Delete(basketName); // sepeti boşaltma

        }

        public void DeleteItem(int notebookId)
        {
           bool response=_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
           if (response) 
           {
                BasketDto basketDto = GetOrCreateBasket();
                BasketItemDto basketItemDto=basketDto.BasketItems.FirstOrDefault(x=>x.NotebookId == notebookId);
                basketDto.BasketItems.Remove(basketItemDto);
                string basketSerialize=JsonConvert.SerializeObject(basketDto);
               _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName,basketSerialize);
           }
        }

        public BasketDto GetOrCreateBasket()
        {
           bool response= _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
           //if (response) 
           //{
           //     return JsonConvert.DeserializeObject<BasketDto>(_context.Request.Cookies[basketName]); 
           //     // kendi tipine dönüştürme c# dan json a çevirme
           //}
           //else 
           //{
           //     return new BasketDto();
           // }

           return response ? JsonConvert.DeserializeObject<BasketDto>(_httpContextAccessor.HttpContext.Request.Cookies[basketName]): new BasketDto();
        }

        public void RemoveOrDecrease(int notebookId)
        {
            bool response=_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
            if (response)
            {
                BasketDto basketDto=GetOrCreateBasket();
                foreach (var item in basketDto.BasketItems)
                {
                    if (item.NotebookId==notebookId && item.Quantity>1)
                    {
                        item.Quantity -= 1;

                    }
                    else basketDto.BasketItems.Remove(item);

                    
                }

                string basketSerialize = JsonConvert.SerializeObject(basketDto);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, basketSerialize);
            }
        }

        public void SaveUpdateBasketItem(BasketItemDto basketItem)
        {
           BasketDto basketDto=GetOrCreateBasket();
            if (basketDto.BasketItems.Any(x=>x.NotebookId==basketItem.NotebookId))
            {
                BasketItemDto basketItemDto=basketDto.BasketItems.FirstOrDefault(x=>x.NotebookId==basketItem.NotebookId);
                basketItem.Quantity += 1;
            }

            else
            
                basketDto.BasketItems.Add(basketItem);

            string basketSerialize=JsonConvert.SerializeObject(basketItem);
           _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, basketSerialize);
            
        }

    }
}
