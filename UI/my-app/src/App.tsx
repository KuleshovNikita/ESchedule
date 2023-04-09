import { useEffect } from 'react';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { useStore } from './api/stores/StoresManager';
import LoadingComponent from './components/hoc/LoadingComponent';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { observer } from 'mobx-react-lite';
import AppRoutes from './components/AppRoutes';
import Header from './components/markups/Header';
import AppLoader from './AppLoader';

function App() {
  const stores = useStore();

  useEffect(() => {
    AppLoader(stores);
  }, [stores.commonStore, 
      stores.userStore,
      stores.groupStore,
      stores.lessonStore,
      stores.groupLessonStore,
      stores.scheduleStore,
      stores.teacherGroupLessonStore,
      stores.teacherLessonStore,
      stores.tenantStore,
      stores.tenantSettingsStore]);

  if (!stores.commonStore.appLoaded) {
    return <LoadingComponent />;
  }

  return (
    <BrowserRouter>
      <React.StrictMode>
        <Header />        
        <main className="main">
          <AppRoutes />
        </main>
        <ToastContainer position={"bottom-right"} hideProgressBar={true}/>
      </React.StrictMode>
    </BrowserRouter>
  );
}

export default observer(App);
