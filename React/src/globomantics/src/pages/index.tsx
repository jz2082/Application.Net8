import { QueryClientProvider, QueryClient } from "@tanstack/react-query";
import axios from "axios";

import App from "@/components/App";

export default function Index()
{
  axios.defaults.withCredentials = true;
  const queryClient = new QueryClient();
  
  return(
    <>
      <QueryClientProvider client={queryClient}>
        <App />
      </QueryClientProvider>
    </>
  );
}