import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios, { AxiosError, AxiosResponse } from "axios";

import Config from "@/config";
import { House } from "@/types/house";
import Problem from "@/types/problem";
import { ApiResponse } from "@/types/apiResponse";

const useFetchHouses = () => {
  return useQuery<ApiResponse<House[]>, AxiosError<Problem>>({
    queryKey: ["houses"],
    queryFn: () =>
      axios
        .get(`${Config.baseApiUrl}/api/house/list`)
        .then((resp) => resp.data),
  });
};

const useFetchHouse = (id: number) => {
  return useQuery<ApiResponse<House>, AxiosError<Problem>>({
    queryKey: ["house", id],
    queryFn: () =>
      axios
        .get(`${Config.baseApiUrl}/house/${id}`)
        .then((resp) => resp.data),
  });
};

const useAddHouse = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>({
    mutationFn: (x) =>
      axios
        .post(`${Config.baseApiUrl}/house`, x),
    onSuccess: (_, house) => {
      queryClient.invalidateQueries({ queryKey: ["houses"] });
    }
  });
};

const useUpdateHouse = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError<Problem>, House>({
    mutationFn: (x) =>
      axios
        .put(`${Config.baseApiUrl}/house`, x),
    onSuccess: (_, house) => {
      queryClient.invalidateQueries({ queryKey: ["house", house.id] });
    },
  });
};

const useDeleteHouse = () => {
  const queryClient = useQueryClient();
  return useMutation<AxiosResponse, AxiosError, House>({
    mutationFn: (h) =>
      axios
        .delete(`${Config.baseApiUrl}/house/${h.id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["houses"] });
    },
  });
};

export {
  useFetchHouses,
  useFetchHouse,
  useAddHouse,
  useUpdateHouse,
  useDeleteHouse
};
