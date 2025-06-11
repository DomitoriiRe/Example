using Controller; 
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public  override void InstallBindings()
    { 
        BindPlayerState();

        BindHandleRotation(); 

        BindShelfItemList();

        BindAssetUnloader();

        BindAssetRegistry();

        BindAddressableLoaderService();

        BindPrefabInstantiatorService(); 
    }

    private void BindPlayerState() => Container.Bind<IPlayerState>().To<PlayerState>().AsSingle();
    private void BindHandleRotation() => Container.Bind<IHandleRotation>().To<HandleRotation>().AsSingle(); 
    private void BindShelfItemList() => Container.Bind<IShelfItemList>().To<ShelfItemList>().AsSingle();
    private void BindAssetUnloader() => Container.Bind<IAssetUnloader>().To<AssetUnloader>().AsSingle();
    private void BindAssetRegistry() => Container.Bind<IAssetRegistry>().To<AssetRegistry>().AsSingle();
    private void BindAddressableLoaderService() => Container.Bind<IAddressableLoaderService>().To<AddressableLoaderService>().AsSingle();
    private void BindPrefabInstantiatorService() => Container.Bind<IPrefabInstantiatorService>().To<PrefabInstantiatorService>().AsSingle();
}
